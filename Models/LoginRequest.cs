namespace JwtTokenApi.Models
{
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }
}
