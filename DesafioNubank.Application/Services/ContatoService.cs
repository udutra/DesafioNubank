using DesafioNubank.Application.DTO.Request.Contato;
using DesafioNubank.Application.DTO.Response.Contato;
using DesafioNubank.Application.Interfaces;

namespace DesafioNubank.Application.Services;

public class ContatoService : IContatoService{
    public Task<ContatoResponseDto> CreateContatoAsync(ContatoCreateDto contatoCreateDto){
        throw new NotImplementedException();
    }
    public Task<IEnumerable<ContatoResponseDto>> GetContatosByClienteIdAsync(Guid clienteId){
        throw new NotImplementedException();
    }
    public Task<ContatoResponseDto?> GetContatoByIdAsync(Guid id){
        throw new NotImplementedException();
    }
    public Task<ContatoResponseDto?> UpdateContatoAsync(Guid id, ContatoUpdateDto contatoUpdateDto){
        throw new NotImplementedException();
    }
    public Task<bool> DeleteContatoAsync(Guid id){
        throw new NotImplementedException();
    }
}