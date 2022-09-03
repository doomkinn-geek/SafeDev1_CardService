using CardService.Data;
using CardService.Models;

namespace CardService.Models
{
    public class SessionInfo
    {
        public int SessionId { get; set; }

        public string SessionToken { get; set; }

        public AccountDto Account { get; set; }
    }
}
