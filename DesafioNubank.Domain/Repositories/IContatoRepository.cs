using DesafioNubank.Domain.Models;

namespace DesafioNubank.Domain.Repositories;

public interface IContatoRepository{
    Task<Contato> GetByIdAsync(Guid id);
    Task AddAsync(Contato contato);
}