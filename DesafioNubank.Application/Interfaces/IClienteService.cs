using DesafioNubank.Application.DTO.Request.Cliente;
using DesafioNubank.Application.DTO.Response.Cliente;

namespace DesafioNubank.Application.Interfaces;

public interface IClienteService{
    
    /// <summary>
    /// Cria um novo cliente.
    /// </summary>
    /// <param name="clienteCreateDto">DTO contendo os dados para criação do cliente.</param>
    /// <returns>DTO do cliente recém-criado.</returns>
    Task<ClienteResponseDto> CreateClienteAsync(ClienteCreateDto clienteCreateDto);
    
    /// <summary>
    /// Lista todos os clientes cadastrados, incluindo seus contatos.
    /// </summary>
    /// <returns>Uma coleção de DTOs de clientes.</returns>
    Task<IEnumerable<ClienteResponseDto>> GetAllClientesAsync();
    
    /// <summary>
    /// Busca um cliente específico pelo seu ID.
    /// </summary>
    /// <param name="id">O ID do cliente a ser buscado.</param>
    /// <returns>DTO do cliente encontrado ou null se não existir.</returns>
    Task<ClienteResponseDto?> GetClienteByIdAsync(Guid id);
    
    /// <summary>
    /// Atualiza os dados de um cliente existente.
    /// </summary>
    /// <param name="id">O ID do cliente a ser atualizado.</param>
    /// <param name="clienteUpdateDto">DTO contendo os dados para atualização.</param>
    /// <returns>DTO do cliente atualizado ou null se o cliente não for encontrado.</returns>
    Task<ClienteResponseDto?> UpdateClienteAsync(Guid id, ClienteUpdateDto clienteUpdateDto); // Você precisará criar ClienteUpdateDto
    
    /// <summary>
    /// Remove um cliente existente.
    /// </summary>
    /// <param name="id">O ID do cliente a ser removido.</param>
    /// <returns>True se o cliente foi removido com sucesso, false caso contrário (ex: cliente não encontrado).</returns>
    Task<bool> DeleteClienteAsync(Guid id);

}