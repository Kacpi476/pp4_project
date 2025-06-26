using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application.Service;

public class ClientService  : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public List<Client> GetClients() => _clientRepository.GetClients();

    public Client? GetClientById(int id) => _clientRepository.GetClientById(id);

    public Client AddClient(Client client)
    {
        if (string.IsNullOrWhiteSpace(client.Name))
            throw new Exception("Imię klienta nie może być puste");

        return _clientRepository.AddClient(client);
    }
}