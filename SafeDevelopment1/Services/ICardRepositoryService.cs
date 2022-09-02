using CardService.Data;

namespace CardService.Services
{
    public interface ICardRepositoryService : IRepository<Card, string>
    {
        IList<Card> GetByClientId(int id);
    }
}
