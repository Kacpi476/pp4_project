using EShop.Domain.Models;

namespace EShop.Application.Service;

public interface IClientService
{
    List<Client> GetClients();
    Client? GetClientById(int id);
    Client AddClient(Client client);
}