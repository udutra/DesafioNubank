using DesafioNubank.Api.Application.DTO.Request.Cliente;
using DesafioNubank.Api.Application.DTO.Response;
using DesafioNubank.Api.Application.DTO.Response.Cliente;
using DesafioNubank.Api.Application.Interfaces;

namespace DesafioNubank.Api.Application.Services;

public class ClienteService : IClienteService{
    public Task<ClienteResponseDto> CreateClienteAsync(ClienteCreateDto clienteCreateDto){
        throw new NotImplementedException();
    }
    public Task<IEnumerable<ClienteResponseDto>> GetAllClientesAsync(){
        throw new NotImplementedException();
    }
    public Task<ClienteResponseDto?> GetClienteByIdAsync(Guid id){
        throw new NotImplementedException();
    }
    public Task<ClienteResponseDto?> UpdateClienteAsync(Guid id, ClienteUpdateDto clienteUpdateDto){
        throw new NotImplementedException();
    }
    public Task<bool> DeleteClienteAsync(Guid id){
        throw new NotImplementedException();
    }
}