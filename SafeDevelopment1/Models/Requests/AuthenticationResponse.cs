namespace CardService.Models.Requests
{
    public class AuthenticationResponse
    {
        public AuthenticationStatus Status { get; set; }
        public SessionInfo SessionInfo { get; set; }
    }
}
