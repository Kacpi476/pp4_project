using EShop.Models;
using EShop.Services;
using Microsoft.AspNetCore.Mvc;
namespace EShop.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public IActionResult GetClients()
    {
        return Ok(_clientService.GetClients());
    }

    [HttpGet("{id}")]
    public IActionResult GetClientById(int id)
    {
        var client = _clientService.GetClientById(id);
        if (client == null)
            return NotFound("Klient nie znaleziony");
        return Ok(client);
    }

    [HttpPost]
    public IActionResult AddClient([FromBody] Client client)
    {
        var newClient = _clientService.AddClient(client);
        return CreatedAtAction(nameof(GetClientById), new { id = newClient.Id }, newClient);
    }
}