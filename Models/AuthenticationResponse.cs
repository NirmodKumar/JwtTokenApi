namespace JwtTokenApi.Models
{
    public class AuthenticationResponse
    {
        public AuthenticationResponse(string token)
        {
            AccessToken=token;
        }

        public string AccessToken { get; set; }
    }
}
