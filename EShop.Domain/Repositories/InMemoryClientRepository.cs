using EShop.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Domain.Repositories
{
    public class InMemoryClientRepository : IClientRepository
    {
        private readonly List<Client> _clients = new();

        public InMemoryClientRepository()
        {
            _clients.Add(new Client { Id = 1, Name = "Jan Kowalski", Email = "jan@mail.com", PhoneNumber = "123-456-789" });
            _clients.Add(new Client { Id = 2, Name = "Anna Nowak", Email = "anna@mail.com", PhoneNumber = "987-654-321" });
        }
        public List<Client> GetClients() => _clients;

        public Client? GetClientById(int id) => _clients.FirstOrDefault(c => c.Id == id);

        public Client AddClient(Client client)
        {
            client.Id = _clients.Count + 1;
            _clients.Add(client);
            return client;
        }
    }
} 