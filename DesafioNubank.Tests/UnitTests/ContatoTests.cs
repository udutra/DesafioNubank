using DesafioNubank.Domain.Models;

namespace DesafioNubank.Tests.UnitTests;


public class ContatoTests{
    
    [Fact]
    [Trait("Category", "UnitTest")]
    public void Construtor_DeveAtribuirPropriedadesCorretamente(){
        // Arrange
        const string nome = "Contato1";
        const string email = "contato@email.com";
        const int ddd = 21;
        const string telefone = "987654321";

        // Act
        var contato = new Contato(nome, email, ddd, telefone);

        // Assert
        Assert.Equal(nome, contato.Nome);
        Assert.Equal(email, contato.Email);
        Assert.Equal(ddd, contato.Ddd);
        Assert.Equal(telefone, contato.Telefone);
        Assert.NotEqual(Guid.Empty, contato.Id);
        Assert.True((DateTime.UtcNow - contato.DataCadastro).TotalSeconds < 5);
        Assert.Equal(Guid.Empty, contato.IdCliente);
        Assert.Null(contato.Cliente);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void Propriedades_PodemSerAlteradas(){
        // Arrange
        var contato = new Contato("A", "a@a.com", 11, "123");
        const string novoNome = "Novo Nome";
        const string novoEmail = "novo@email.com";
        const int novoDdd = 31;
        const string novoTelefone = "111111111";
        var novoIdCliente = Guid.CreateVersion7();
        var cliente = new Cliente("Cliente", "c@c.com", "senha");

        // Act
        contato.Nome = novoNome;
        contato.Email = novoEmail;
        contato.Ddd = novoDdd;
        contato.Telefone = novoTelefone;
        contato.IdCliente = novoIdCliente;
        contato.Cliente = cliente;

        // Assert
        Assert.Equal(novoNome, contato.Nome);
        Assert.Equal(novoEmail, contato.Email);
        Assert.Equal(novoDdd, contato.Ddd);
        Assert.Equal(novoTelefone, contato.Telefone);
        Assert.Equal(novoIdCliente, contato.IdCliente);
        Assert.Equal(cliente, contato.Cliente);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void Construtor_ComValoresNulosOuVazios_DevePermitirCriacaoMasPropriedadesFicamNulasOuVazias(){
        // Arrange & Act
        var contatoNomeNulo = new Contato(null, "email@email.com", 11, "123456789");
        var contatoEmailNulo = new Contato("Nome", null, 11, "123456789");
        var contatoTelefoneNulo = new Contato("Nome", "email@email.com", 11, null);
        var contatoNomeVazio = new Contato("", "email@email.com", 11, "123456789");
        var contatoEmailVazio = new Contato("Nome", "", 11, "123456789");
        var contatoTelefoneVazio = new Contato("Nome", "email@email.com", 11, "");

        // Assert
        Assert.Null(contatoNomeNulo.Nome);
        Assert.Null(contatoEmailNulo.Email);
        Assert.Null(contatoTelefoneNulo.Telefone);
        Assert.Equal("", contatoNomeVazio.Nome);
        Assert.Equal("", contatoEmailVazio.Email);
        Assert.Equal("", contatoTelefoneVazio.Telefone);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void Construtor_ComDddNegativo_DevePermitirCriacaoMasDddFicaNegativo(){
        // Arrange & Act
        var contato = new Contato("Nome", "email@email.com", -1, "123456789");
        // Assert
        Assert.Equal(-1, contato.Ddd);
    }

    [Fact]
    [Trait("Category", "UnitTest")]
    public void Propriedades_PodemSerAlteradasParaValoresInvalidos(){
        // Arrange
        var contato = new Contato("A", "a@a.com", 11, "123"){
            // Act
            Nome = null,
            Email = "",
            Telefone = null,
            Ddd = -99
        };

        // Assert
        Assert.Null(contato.Nome);
        Assert.Equal("", contato.Email);
        Assert.Null(contato.Telefone);
        Assert.Equal(-99, contato.Ddd);
    }
}