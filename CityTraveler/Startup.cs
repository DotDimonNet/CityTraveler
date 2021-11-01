using CityTraveler.Domain.Entities;
using CityTraveler.Services.Interfaces;
using CityTraveler.Services;
using CityTraveler.Infrastructure.Authorization;
using CityTraveler.Infrastructure.Settings;
using CityTraveler.Infrastucture.Data;
using CityTraveler.Services;
using CityTraveler.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CityTraveler
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });
            services.AddDbContext<ApplicationContext>(options =>
            {
                options
                    .UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlOpts => sqlOpts.EnableRetryOnFailure());
            });
            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ApplicationContext>());
            services.Configure<DataProtectionTokenProviderOptions>(x => x.TokenLifespan = TimeSpan.FromMinutes(15));
            services.AddIdentity<ApplicationUserModel, ApplicationUserRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                .AddTokenProvider<PasswordResetTokenProvider>(nameof(PasswordResetTokenProvider));

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
                options.User.AllowedUserNameCharacters = options.User.AllowedUserNameCharacters + "åæøÅÆØ";
                options.Tokens.PasswordResetTokenProvider = nameof(PasswordResetTokenProvider);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.Audience = Configuration["Auth:JwtBearer:audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:JwtBearer:secret"])),
                    ValidIssuer = Configuration["Auth:JwtBearer:issuer"],
                    ValidAudience = Configuration["Auth:JwtBearer:audience"],
                    ClockSkew = TimeSpan.Zero, //Remove delay of token when expire
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.RequireAdminRole, policy => policy.RequireClaim(ClaimTypes.Role, Roles.Admin));
                options.AddPolicy(Policies.RequireContentManagerRole, policy => policy.RequireClaim(ClaimTypes.Role, Roles.ContentManager));
                options.AddPolicy(Policies.RequireUserRole, policy => policy.RequireClaim(ClaimTypes.Role, Roles.User));
            });
            services.AddOptions();
            services.AddTransient<IUserManagementService, UserManagementService>();
            services.AddScoped<DbInitializer>();
            services.AddTransient<ITripService, TripService>();
            services.AddTransient<IEntertainmentService, EntertainmentService>();
            services.AddTransient<ICityArchitectureService, CityArchitectureService>();
            services.AddTransient<ISocialMediaService, SocialMediaService>();
            services.Configure<AuthSettings>(Configuration.GetSection("Auth"));
            services.AddMvc();
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);



            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddSwaggerDocument(configure => configure.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "CityTraveler API";
                document.DocumentPath = "/swagger";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseSpaStaticFiles();
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });

            dbInitializer.Initialize().Wait();
        }
    }

    public class PasswordResetTokenProvider : TotpSecurityStampBasedTokenProvider<ApplicationUserModel>
    {
        public override Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<ApplicationUserModel> manager, ApplicationUserModel user)
        {
            return Task.FromResult(false);
        }

        public override Task<string> GetUserModifierAsync(string purpose, UserManager<ApplicationUserModel> manager, ApplicationUserModel user)
        {
            return Task.FromResult($"{nameof(PasswordResetTokenProvider)}{purpose}{user.Id}");
        }
    }
}
