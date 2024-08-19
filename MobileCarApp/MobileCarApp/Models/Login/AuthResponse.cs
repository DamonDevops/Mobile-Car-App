using MobileCarApp.Models.Base;

namespace MobileCarApp.Models.Login
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }
}
