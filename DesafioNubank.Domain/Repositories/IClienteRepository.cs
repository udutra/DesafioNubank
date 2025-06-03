using DesafioNubank.Domain.Models;

namespace DesafioNubank.Domain.Repositories;

public interface IClienteRepository{

    /// <summary>
    /// Adiciona um novo cliente ao contexto.
    /// </summary>
    Task AddAsync(Cliente cliente);

    /// <summary>
    /// Obtém um cliente pelo seu ID, sem necessariamente incluir coleções aninhadas.
    /// </summary>
    Task<Cliente?> GetByIdAsync(Guid id);

    /// <summary>
    /// Obtém um cliente pelo seu ID, incluindo seus contatos.
    /// </summary>
    Task<Cliente?> GetByIdWithContatosAsync(Guid id);

    /// <summary>
    /// Obtém todos os clientes, incluindo seus contatos.
    /// </summary>
    Task<IEnumerable<Cliente>> GetAllWithContatosAsync();

    /// <summary>
    /// Marca um cliente como modificado no contexto.
    /// </summary>
    void Update(Cliente cliente);

    /// <summary>
    /// Marca um cliente para remoção do contexto.
    /// </summary>
    void Delete(Cliente cliente);

    /// <summary>
    /// Salva todas as alterações feitas no contexto para o banco de dados.
    /// </summary>
    /// <returns>O número de entidades afetadas no banco de dados.</returns>
    Task<int> SaveChangesAsync();
}