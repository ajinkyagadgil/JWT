namespace JWTAuth.Models
{
    public class User
    {
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }

    public static class Roles
    {
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
