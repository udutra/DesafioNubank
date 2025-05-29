using DesafioNubank.Api.Application.DTO.Request.Contato;
using DesafioNubank.Api.Application.DTO.Response.Contato;

namespace DesafioNubank.Api.Application.Interfaces;

public interface IContatoService // Recomendo renomear de IContatoInterface para IContatoService
    {
        /// <summary>
        /// Cria um novo contato associado a um cliente existente.
        /// </summary>
        /// <param name="contatoCreateDto">DTO contendo os dados para criação do contato, incluindo o IdCliente.</param>
        /// <returns>DTO do contato recém-criado.</returns>
        /// <exception cref="ClienteNaoEncontradoException">Lançada se o IdCliente fornecido não corresponder a um cliente existente.</exception>
        Task<ContatoResponseDto> CreateContatoAsync(ContatoCreateDto contatoCreateDto);

        /// <summary>
        /// Lista todos os contatos de um cliente específico.
        /// </summary>
        /// <param name="clienteId">O ID do cliente cujos contatos serão listados.</param>
        /// <returns>Uma coleção de DTOs de contatos pertencentes ao cliente especificado.</returns>
        Task<IEnumerable<ContatoResponseDto>> GetContatosByClienteIdAsync(Guid clienteId);

        /// <summary>
        /// Busca um contato específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do contato a ser buscado.</param>
        /// <returns>DTO do contato encontrado ou null se não existir.</returns>
        Task<ContatoResponseDto?> GetContatoByIdAsync(Guid id);

        /// <summary>
        /// Atualiza os dados de um contato existente.
        /// O cliente associado (IdCliente) geralmente não é alterado por este método.
        /// </summary>
        /// <param name="id">O ID do contato a ser atualizado.</param>
        /// <param name="contatoUpdateDto">DTO contendo os dados para atualização.</param>
        /// <returns>DTO do contato atualizado ou null se o contato não for encontrado.</returns>
        Task<ContatoResponseDto?> UpdateContatoAsync(Guid id, ContatoUpdateDto contatoUpdateDto); // Você precisará criar ContatoUpdateDto

        /// <summary>
        /// Remove um contato existente.
        /// </summary>
        /// <param name="id">O ID do contato a ser removido.</param>
        /// <returns>True se o contato foi removido com sucesso, false caso contrário (ex: contato não encontrado).</returns>
        Task<bool> DeleteContatoAsync(Guid id);
    }