using DesafioNubank.Api.Domain.Models;
using DesafioNubank.Api.Domain.Repositories;

namespace DesafioNubank.Api.Infrastructure.Repositories;

public class ContatoRepository : IContatoRepository{
    public Task<Contato> GetByIdAsync(Guid id){
        throw new NotImplementedException();
    }
    public Task AddAsync(Contato contato){
        throw new NotImplementedException();
    }
}