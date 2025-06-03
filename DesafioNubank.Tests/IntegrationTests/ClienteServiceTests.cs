using AutoMapper;
using DesafioNubank.Application.DTO.Request.Cliente;
using DesafioNubank.Application.DTO.Response.Cliente;
using DesafioNubank.Application.DTO.Response.Contato;
using DesafioNubank.Application.Services;
using DesafioNubank.Domain.Models;
using DesafioNubank.Domain.Repositories;
using Moq;

namespace DesafioNubank.Tests.IntegrationTests;

public class ClienteServiceTests{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly ClienteService _clienteService;

    public ClienteServiceTests(){
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _mapperMock = new Mock<IMapper>();
        _clienteService = new ClienteService(_clienteRepositoryMock.Object, _mapperMock.Object);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task CreateClienteAsync_DeveCriarClienteERetornarDto(){
        // Arrange
        var createDto = new ClienteCreateDto("João", "joao@email.com", "senha123");
        var cliente = new Cliente("João", "joao@email.com", "senha123");
        var responseDto = new ClienteResponseDto(cliente.Id, cliente.Nome, cliente.DataCadastro,
            cliente.Contatos.Select(c => new ContatoResponseDto(c.Id, c.Nome, c.Email, c.Ddd, c.Telefone, c.DataCadastro, c.IdCliente)).ToList());

        _mapperMock.Setup(m => m.Map<Cliente>(createDto)).Returns(cliente);
        _clienteRepositoryMock.Setup(r => r.AddAsync(cliente)).Returns(Task.CompletedTask);
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(cliente)).Returns(responseDto);

        // Act
        var result = await _clienteService.CreateClienteAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("João", result.Nome);
        _clienteRepositoryMock.Verify(r => r.AddAsync(cliente), Times.Once);
        _clienteRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task GetAllClientesAsync_DeveRetornarListaDeClientes(){
        // Arrange
        var clientes = new List<Cliente> { new ("João", "joao@email.com", "senha123")};

        var responseDtos = new List<ClienteResponseDto>{
            new(clientes[0].Id, clientes[0].Nome, clientes[0].DataCadastro,
                clientes[0].Contatos.Select(c =>
                        new ContatoResponseDto(c.Id, c.Nome, c.Email, c.Ddd, c.Telefone, c.DataCadastro, c.IdCliente))
                    .ToList())
        };
    
        _clienteRepositoryMock.Setup(r => r.GetAllWithContatosAsync()).ReturnsAsync(clientes);
        _mapperMock.Setup(m => m.Map<IEnumerable<ClienteResponseDto>>(clientes)).Returns(responseDtos);

        // Act
        var result = await _clienteService.GetAllClientesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
        Assert.Equal("João", ((List<ClienteResponseDto>)result)[0].Nome);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task GetClienteByIdAsync_ClienteExistente_DeveRetornarCliente(){
        
        // Arrange
        var id = Guid.CreateVersion7();
        var cliente = new Cliente("João", "joao@email.com", "senha123");
        var responseDto = new ClienteResponseDto(cliente.Id, cliente.Nome, cliente.DataCadastro,
            cliente.Contatos.Select(c => new ContatoResponseDto(c.Id, c.Nome, c.Email, c.Ddd, c.Telefone, c.DataCadastro, c.IdCliente)).ToList());
        
        _clienteRepositoryMock.Setup(r => r.GetByIdWithContatosAsync(id)).ReturnsAsync(cliente);
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(cliente)).Returns(responseDto);

        // Act
        var result = await _clienteService.GetClienteByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("João", result.Nome);
    }

    
    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task GetClienteByIdAsync_ClienteInexistente_DeveRetornarNull(){
        // Arrange
        var id = Guid.CreateVersion7();
        _clienteRepositoryMock.Setup(r => r.GetByIdWithContatosAsync(id)).ReturnsAsync((Cliente?)null);

        // Act
        var result = await _clienteService.GetClienteByIdAsync(id);

        // Assert
        Assert.Null(result);
    }

    
    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task UpdateClienteAsync_ClienteExistente_DeveAtualizarERetornarDto()
    {
        // Arrange
        var id = Guid.CreateVersion7();
        var updateDto = new ClienteUpdateDto("Novo Nome", "novoemail@email.com", "novaSenha"); 

        var clienteExistente = new Cliente ("Antigo Nome", "antigoemail@email.com", "antigaSenha"); 
        var clienteAtualizado = new Cliente (updateDto.Nome, updateDto.Email, updateDto.Password); 


        var responseDto = new ClienteResponseDto(clienteAtualizado.Id, clienteAtualizado.Nome, clienteAtualizado.DataCadastro,
            clienteAtualizado.Contatos.Select(c => new ContatoResponseDto(c.Id, c.Nome, c.Email, c.Ddd, c.Telefone, c.DataCadastro, c.IdCliente)).ToList());
        

        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(clienteExistente);
        _mapperMock.Setup(m => m.Map(updateDto, clienteExistente)).Returns(clienteAtualizado);
        _clienteRepositoryMock.Setup(r => r.Update(clienteExistente));
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
        _clienteRepositoryMock.Setup(r => r.GetByIdWithContatosAsync(id)).ReturnsAsync(clienteAtualizado);
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(clienteAtualizado)).Returns(responseDto);

        // Act
        var result = await _clienteService.UpdateClienteAsync(id, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Novo Nome", result.Nome);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task UpdateClienteAsync_ClienteInexistente_DeveRetornarNull(){
        // Arrange
        var id = Guid.CreateVersion7();
        var updateDto = new ClienteUpdateDto("Novo Nome", "novoemail@email.com", "novaSenha"); 
        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Cliente?)null);

        // Act
        var result = await _clienteService.UpdateClienteAsync(id, updateDto);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task DeleteClienteAsync_ClienteExistente_DeveDeletarERetornarTrue()
    {
        // Arrange
        var id = Guid.CreateVersion7();
        var cliente = new Cliente("Para Deletar", "joao@email.com", "senha123");
        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(cliente);
        _clienteRepositoryMock.Setup(r => r.Delete(cliente));
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _clienteService.DeleteClienteAsync(id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task DeleteClienteAsync_ClienteInexistente_DeveRetornarFalse()
    {
        // Arrange
        var id = Guid.CreateVersion7();
        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Cliente?)null);

        // Act
        var result = await _clienteService.DeleteClienteAsync(id);

        // Assert
        Assert.False(result);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task CreateClienteAsync_ClienteCreateDtoNulo_DeveLancarArgumentNullException(){
        await Assert.ThrowsAsync<ArgumentNullException>(() => _clienteService.CreateClienteAsync(null!));
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task UpdateClienteAsync_ClienteUpdateDtoNulo_DeveLancarArgumentNullException(){
        var id = Guid.CreateVersion7();
        await Assert.ThrowsAsync<ArgumentNullException>(() => _clienteService.UpdateClienteAsync(id, null!));
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task CreateClienteAsync_FalhaAoSalvar_DeveRetornarDtoMesmoAssim(){
        // Arrange
        var createDto = new ClienteCreateDto("João", "joao@email.com", "senha123");
        var cliente = new Cliente("João", "joao@email.com", "senha123");
        var responseDto = new ClienteResponseDto(cliente.Id, cliente.Nome, cliente.DataCadastro, new List<ContatoResponseDto>());
        _mapperMock.Setup(m => m.Map<Cliente>(createDto)).Returns(cliente);
        _clienteRepositoryMock.Setup(r => r.AddAsync(cliente)).Returns(Task.CompletedTask);
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(0); // Falha ao salvar
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(cliente)).Returns(responseDto);
        // Act
        var result = await _clienteService.CreateClienteAsync(createDto);
        // Assert
        Assert.NotNull(result);
        Assert.Equal("João", result.Nome);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task CreateClienteAsync_RepositorioLancaExcecao_DevePropagarExcecao(){
        // Arrange
        var createDto = new ClienteCreateDto("João", "joao@email.com", "senha123");
        _mapperMock.Setup(m => m.Map<Cliente>(createDto)).Throws(new InvalidOperationException("Erro no mapper"));
        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => _clienteService.CreateClienteAsync(createDto));
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task UpdateClienteAsync_DeveChamarMapperParaAtualizarEntidadeExistente(){
        // Arrange
        var id = Guid.CreateVersion7();
        var updateDto = new ClienteUpdateDto("Novo Nome", "novo@email.com", "novaSenha");
        var clienteExistente = new Cliente("Antigo Nome", "antigo@email.com", "antigaSenha");
        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(clienteExistente);
        _mapperMock.Setup(m => m.Map(updateDto, clienteExistente)).Returns(clienteExistente);
        _clienteRepositoryMock.Setup(r => r.Update(clienteExistente));
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
        _clienteRepositoryMock.Setup(r => r.GetByIdWithContatosAsync(id)).ReturnsAsync(clienteExistente);
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(clienteExistente)).Returns(new ClienteResponseDto(clienteExistente.Id, clienteExistente.Nome, clienteExistente.DataCadastro, new List<ContatoResponseDto>()));
        // Act
        var result = await _clienteService.UpdateClienteAsync(id, updateDto);
        // Assert
        _mapperMock.Verify(m => m.Map(updateDto, clienteExistente), Times.Once);
        Assert.NotNull(result);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task GetClienteByIdAsync_ClienteComContatos_DeveRetornarDtoComContatos(){
        // Arrange
        var id = Guid.CreateVersion7();
        var cliente = new Cliente("João", "joao@email.com", "senha123");
        var contato = new Contato("Contato1", "c1@c.com", 11, "999999999");
        cliente.Contatos.Add(contato);
        var contatoDto = new ContatoResponseDto(contato.Id, contato.Nome, contato.Email, contato.Ddd, contato.Telefone, contato.DataCadastro, cliente.Id);
        var responseDto = new ClienteResponseDto(cliente.Id, cliente.Nome, cliente.DataCadastro, new List<ContatoResponseDto>{contatoDto});
        _clienteRepositoryMock.Setup(r => r.GetByIdWithContatosAsync(id)).ReturnsAsync(cliente);
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(cliente)).Returns(responseDto);
        // Act
        var result = await _clienteService.GetClienteByIdAsync(id);
        // Assert
        Assert.NotNull(result);
        Assert.Single(result.Contatos);
        Assert.Equal("Contato1", result.Contatos.First().Nome);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task UpdateClienteAsync_NaoAlteraDataCadastro(){
        // Arrange
        var id = Guid.CreateVersion7();
        var dataCadastroOriginal = DateTime.UtcNow.AddDays(-10);
        var clienteExistente = new Cliente("Antigo Nome", "antigo@email.com", "antigaSenha") { DataCadastro = dataCadastroOriginal };
        var updateDto = new ClienteUpdateDto("Novo Nome", "novo@email.com", "novaSenha");
        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(clienteExistente);
        _mapperMock.Setup(m => m.Map(updateDto, clienteExistente)).Returns(clienteExistente);
        _clienteRepositoryMock.Setup(r => r.Update(clienteExistente));
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
        _clienteRepositoryMock.Setup(r => r.GetByIdWithContatosAsync(id)).ReturnsAsync(clienteExistente);
        _mapperMock.Setup(m => m.Map<ClienteResponseDto>(clienteExistente)).Returns(new ClienteResponseDto(clienteExistente.Id, clienteExistente.Nome, clienteExistente.DataCadastro, new List<ContatoResponseDto>()));
        // Act
        var result = await _clienteService.UpdateClienteAsync(id, updateDto);
        // Assert
        Assert.Equal(dataCadastroOriginal, clienteExistente.DataCadastro);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task DeleteClienteAsync_ClienteComContatos_DevePermitirDeletar(){
        // Arrange
        var id = Guid.CreateVersion7();
        var cliente = new Cliente("Para Deletar", "email@email.com", "senha");
        cliente.Contatos.Add(new Contato("Contato1", "c1@c.com", 11, "999999999"));
        _clienteRepositoryMock.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(cliente);
        _clienteRepositoryMock.Setup(r => r.Delete(cliente));
        _clienteRepositoryMock.Setup(r => r.SaveChangesAsync()).ReturnsAsync(1);
        // Act
        var result = await _clienteService.DeleteClienteAsync(id);
        // Assert
        Assert.True(result);
    }

    [Fact]
    [Trait("Category", "IntegrationTest")]
    public async Task GetAllClientesAsync_DeveRetornarClientes(){
        // Arrange
        var clientes = new List<Cliente> { new ("João", "joao@email.com", "senha123")};
        var responseDtos = new List<ClienteResponseDto>{ new(clientes[0].Id, clientes[0].Nome, clientes[0].DataCadastro, new List<ContatoResponseDto>()) };
        _clienteRepositoryMock.Setup(r => r.GetAllWithContatosAsync()).ReturnsAsync(clientes);
        _mapperMock.Setup(m => m.Map<IEnumerable<ClienteResponseDto>>(clientes)).Returns(responseDtos);

        // Act
        var result = await _clienteService.GetAllClientesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Single(result);
    }
}
