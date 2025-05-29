using DesafioNubank.Api.Domain.Models;

namespace DesafioNubank.Api.Domain.Repositories;

public interface IContatoRepository{
    Task<Contato> GetByIdAsync(Guid id);
    Task AddAsync(Contato contato);
}