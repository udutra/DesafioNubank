using DesafioNubank.Application.DTO.Request.Cliente;
using DesafioNubank.Application.DTO.Response.Cliente;
using DesafioNubank.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DesafioNubank.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController(IClienteService clienteService) : ControllerBase{
    [HttpPost]
    public async Task<ActionResult<ClienteResponseDto>> CreateCliente([FromBody] ClienteCreateDto clienteCreateDto){
        try
        {
            var novoCliente = await clienteService.CreateClienteAsync(clienteCreateDto);
           return CreatedAtAction(nameof(GetClientePorId), new { id = novoCliente.Id }, novoCliente);
        }
        catch (Exception ex){// Exemplo: pegar exceções específicas do serviço, como "ClienteJaExisteException"
            // Log ex
            return StatusCode(500, "Ocorreu um erro ao tentar criar o cliente.");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClienteResponseDto>> GetClientePorId(Guid id){
        var cliente = await clienteService.GetClienteByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return Ok(cliente);
    }
}