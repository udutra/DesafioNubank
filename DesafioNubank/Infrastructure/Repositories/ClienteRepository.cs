using DesafioNubank.Api.Domain.Models;
using DesafioNubank.Api.Domain.Repositories;

namespace DesafioNubank.Api.Infrastructure.Repositories;

public class ClienteRepository: IClienteRepository{
    public Task<Cliente> GetByIdAsync(Guid id){
        throw new NotImplementedException();
    }
    public Task<List<Cliente>> GetAllClients(){
        throw new NotImplementedException();
    }
    public Task<List<Contato>> GetAllContacts(Guid id){
        throw new NotImplementedException();
    }
    public Task AddAsync(Cliente cliente){
        throw new NotImplementedException();
    }
}