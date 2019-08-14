namespace TestApp.Core.Entities
{
    public class UserRole
    {
        public string RoleId { get; set; }

        public string UserId { get; set; }

        public Role Role { get; set; }

        public User User { get; set; }
    }
}
