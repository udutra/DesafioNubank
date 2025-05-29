using DesafioNubank.Api.Domain.Models;

namespace DesafioNubank.Api.Domain.Repositories;

public interface IClienteRepository{
    Task<Cliente> GetByIdAsync(Guid id);
    Task<List<Cliente>> GetAllClients();
    Task<List<Contato>> GetAllContacts(Guid id);
    Task AddAsync(Cliente cliente);
}