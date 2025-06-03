using DesafioNubank.Domain.Models;
using DesafioNubank.Domain.Repositories;

namespace DesafioNubank.Infrastructure.Repositories;

public class ContatoRepository : IContatoRepository{
    public Task<Contato> GetByIdAsync(Guid id){
        throw new NotImplementedException();
    }
    public Task AddAsync(Contato contato){
        throw new NotImplementedException();
    }
}