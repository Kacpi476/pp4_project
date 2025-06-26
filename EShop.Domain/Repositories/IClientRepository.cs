using EShop.Domain.Models;

namespace EShop.Domain.Repositories;

public interface IClientRepository
{
    List<Client> GetClients();
    Client? GetClientById(int id);
    Client AddClient(Client client);
}