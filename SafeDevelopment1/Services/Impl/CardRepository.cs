using CardService.Data;

namespace CardService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        private readonly CardServiceDbContext _context;
        private readonly ILogger<CardRepository> _logger;

        public CardRepository(
            ILogger<CardRepository> logger,            
            CardServiceDbContext context)
        {
            _logger = logger;            
            _context = context;
        }
        public string Create(Card data)
        {
            var client = _context.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Клиент не найден");

            _context.Cards.Add(data);
            _context.SaveChanges();
            return data.CardId.ToString();
        }

        public int Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IList<Card> GetAll()
        {
            throw new NotImplementedException();
        }

        public Card GetById(string id)
        {
            throw new NotImplementedException();
        }

        public int Update(Card data)
        {
            throw new NotImplementedException();
        }
    }
}
