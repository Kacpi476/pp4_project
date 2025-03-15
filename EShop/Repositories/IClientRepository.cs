using EShop.Models;

namespace EShop.Repositories;

public interface IClientRepository
{
    List<Client> GetClients();
    Client? GetClientById(int id);
    Client AddClient(Client client);
}