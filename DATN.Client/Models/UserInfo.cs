namespace DATN.Client.Models
{
    public class UserInfo
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Role { get; set; }
        public int RoleNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
