namespace CityTraveler.Infrastructure.Authorization
{
    public struct Roles
    {
        public const string Admin = "Admin";
        public const string ContentManager = "ContentManager";
        public const string User = "User";
        public const string Guest = "Guest";
    }

    public struct Policies
    {
        public const string RequireAdminRole = "RequireAdminRole";
        public const string RequireContentManagerRole = "RequireContentManagerRole";
        public const string RequireUserRole = "RequireUserRole";
    }
}
