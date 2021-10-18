namespace CityTraveler.Infrastructure.Settings
{
    public class AuthSettings
    {
        public JwtBearerAuth JwtBearer { get; set; }
        public class JwtBearerAuth
        {
            public string Authority { get; set; }
            public string Audience { get; set; }
            public string Issuer { get; set; }
            public string Secret { get; set; }
            public int AuthTokenValid { get; set; }
            public int RefreshTokenValid { get; set; }
        }
    }
}
