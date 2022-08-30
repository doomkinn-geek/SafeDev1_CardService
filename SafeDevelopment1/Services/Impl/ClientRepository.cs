using CardService.Data;

namespace CardService.Services.Impl
{
    public class ClientRepository : IClientRepositoryService
    {
        private readonly CardServiceDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(
            ILogger<ClientRepository> logger,
            CardServiceDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public int Create(Client data)
        {
            _context.Clients.Add(data);
            _context.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Client data)
        {
            throw new NotImplementedException();
        }
    }
}
