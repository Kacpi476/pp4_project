using EShop.Models;

namespace EShop.Services;

public interface IClientService
{
    List<Client> GetClients();
    Client? GetClientById(int id);
    Client AddClient(Client client);
}