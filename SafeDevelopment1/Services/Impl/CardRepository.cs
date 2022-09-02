using CardService.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CardService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        private readonly CardServiceDbContext _context;
        private readonly ILogger<CardRepository> _logger;
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        public CardRepository(
            ILogger<CardRepository> logger,            
            CardServiceDbContext context,
            IOptions<DatabaseOptions> databaseOptions)
        {
            _logger = logger;
            _context = context;
            _databaseOptions = databaseOptions;
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

        public IList<Card> GetByClientId(int id)
        {
            /*List<Card> cards = new List<Card>();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select * from cards where ClientId = {0}", id), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new Card
                        {
                            CardId = new Guid(reader["CardId"].ToString()),
                            CardNo = reader["CardNo"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            CVV2 = reader["CVV2"]?.ToString(),
                            ExpDate = Convert.ToDateTime(reader["ExpDate"])
                        });
                    }
                }

            }
            return cards;*/
            return _context.Cards.Where(card => card.ClientId == id).ToList();
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
