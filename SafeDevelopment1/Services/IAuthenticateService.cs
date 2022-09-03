using CardService.Models;
using CardService.Models.Requests;

namespace CardService.Services
{
    public interface IAuthenticateService
    {
        AuthenticationResponse Login(AuthenticationRequest authenticationRequest);

        public SessionInfo GetSessionInfo(string sessionToken);

    }
}
