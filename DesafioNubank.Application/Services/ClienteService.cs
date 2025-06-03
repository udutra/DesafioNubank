using AutoMapper;
using DesafioNubank.Application.DTO.Request.Cliente;
using DesafioNubank.Application.DTO.Response.Cliente;
using DesafioNubank.Application.Interfaces;
using DesafioNubank.Domain.Models;
using DesafioNubank.Domain.Repositories;

namespace DesafioNubank.Application.Services;

public class ClienteService(IClienteRepository clienteRepository, IMapper mapper) : IClienteService{
    
    private readonly IClienteRepository _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    // _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext)); // Exemplo
    // _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork)); // Exemplo

    public async Task<ClienteResponseDto> CreateClienteAsync(ClienteCreateDto clienteCreateDto){
        ArgumentNullException.ThrowIfNull(clienteCreateDto);
        
        var clienteEntity = _mapper.Map<Cliente>(clienteCreateDto);
        await _clienteRepository.AddAsync(clienteEntity);
        await _clienteRepository.SaveChangesAsync();
        var clienteResponseDto = _mapper.Map<ClienteResponseDto>(clienteEntity);
        return clienteResponseDto;
    }
    
    public async Task<IEnumerable<ClienteResponseDto>> GetAllClientesAsync()
    {
        // Buscar todos os clientes (o repositório deve carregar idealmente os contatos se forem necessários)
        var clientes = await _clienteRepository.GetAllWithContatosAsync(); // Supondo um método que já inclua contatos

        // Mapear a lista de entidades para uma lista de DTOs de resposta
        // Se não usar AutoMapper:
        // var clienteResponseDtos = clientes.Select(c => new ClienteResponseDto(
        // c.Id, c.Nome, c.DataCadastro,
        // c.Contatos.Select(ct => new ContatoResponseDto(ct.Id, ct.Nome, ct.Email, ct.Ddd, ct.Telefone, ct.DataCadastro)).ToList()
        // )).ToList();
        var clienteResponseDtos = _mapper.Map<IEnumerable<ClienteResponseDto>>(clientes);

        return clienteResponseDtos;
    }
    
    public async Task<ClienteResponseDto?> GetClienteByIdAsync(Guid id){
        // Buscar cliente por ID (o repositório deve idealmente carregar os contatos)
        var cliente = await _clienteRepository.GetByIdWithContatosAsync(id); // Supondo um método que já inclua contatos

        if (cliente == null)
        {
            return null; // Ou lançar uma exceção ClienteNaoEncontradoException
        }

        // Mapear entidade para DTO de resposta
        var clienteResponseDto = _mapper.Map<ClienteResponseDto>(cliente);

        return clienteResponseDto;
    }
    
    public async Task<ClienteResponseDto?> UpdateClienteAsync(Guid id, ClienteUpdateDto clienteUpdateDto)
    {
        ArgumentNullException.ThrowIfNull(clienteUpdateDto);

        var clienteExistente = await _clienteRepository.GetByIdAsync(id); // Pode não precisar dos contatos para update do cliente em si

        if (clienteExistente == null)
        {
            return null; // Ou lançar uma exceção ClienteNaoEncontradoException
        }

        // Mapear/Aplicar as alterações do DTO para a entidade existente
        // Se não usar AutoMapper para mapear um DTO para uma entidade existente:
        // clienteExistente.Nome = clienteUpdateDto.Nome;
        // /* atualizar outras propriedades permitidas */
        _mapper.Map(clienteUpdateDto, clienteExistente); // AutoMapper pode mapear para um objeto existente

        _clienteRepository.Update(clienteExistente); // EF Core rastreia mudanças, mas chamar Update pode ser explícito.

        // Salvar alterações
        // await _dbContext.SaveChangesAsync();
        // ou
        // await _unitOfWork.SaveChangesAsync();
        await _clienteRepository.SaveChangesAsync();

        // Recarregar o cliente com contatos para a resposta, se necessário e se não foram atualizados no objeto 'clienteExistente'
        // ou simplesmente mapear 'clienteExistente' se o mapeador estiver configurado para carregar/mapear os contatos.
        // Para garantir que a resposta tenha os contatos (se GetByIdAsync não os carregou ou se foram alterados):
        var clienteAtualizadoComContatos = await _clienteRepository.GetByIdWithContatosAsync(id);
        var clienteResponseDto = _mapper.Map<ClienteResponseDto>(clienteAtualizadoComContatos);


        return clienteResponseDto;
    }
    
    public async Task<bool> DeleteClienteAsync(Guid id)
    {
        var clienteParaDeletar = await _clienteRepository.GetByIdAsync(id); // Usa GetByIdAsync, pois não precisamos dos contatos para deletar

        if (clienteParaDeletar == null){
            return false; // Ou lançar ClienteNaoEncontradoException
        }
        _clienteRepository.Delete(clienteParaDeletar); // Marca para deleção (síncrono)
        var alteracoesSalvas = await _clienteRepository.SaveChangesAsync(); // Persiste a deleção (assíncrono)
        return alteracoesSalvas > 0; // Retorna true se pelo menos uma entidade foi afetada
    }
}