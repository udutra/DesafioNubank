using DesafioNubank.Domain.Models;

namespace DesafioNubank.Tests.UnitTests
{
    public class ClienteTests{
        
        [Fact]
        [Trait("Category", "UnitTest")]
        public void Construtor_DeveAtribuirPropriedadesCorretamente(){
            // Arrange
            const string nome = "João";
            const string email = "joao@email.com";
            const string password = "senha123";

            // Act
            var cliente = new Cliente(nome, email, password);

            // Assert
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(email, cliente.Email);
            Assert.Equal(password, cliente.Password);
            Assert.NotEqual(Guid.Empty, cliente.Id);
            Assert.True((DateTime.UtcNow - cliente.DataCadastro).TotalSeconds < 5);
            Assert.Null(cliente.DataUltimaAlteracao);
            Assert.NotNull(cliente.Contatos);
            Assert.Empty(cliente.Contatos);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void Construtor_ComValoresNulosOuVazios_DevePermitirCriacaoMasPropriedadesFicamNulasOuVazias(){
            // Arrange & Act
            var clienteNomeNulo = new Cliente(null!, "email@email.com", "senha");
            var clienteEmailNulo = new Cliente("Nome", null!, "senha");
            var clienteSenhaNula = new Cliente("Nome", "email@email.com", null!);
            var clienteNomeVazio = new Cliente("", "email@email.com", "senha");
            var clienteEmailVazio = new Cliente("Nome", "", "senha");
            var clienteSenhaVazia = new Cliente("Nome", "email@email.com", "");

            // Assert
            Assert.Null(clienteNomeNulo.Nome);
            Assert.Null(clienteEmailNulo.Email);
            Assert.Null(clienteSenhaNula.Password);
            Assert.Equal("", clienteNomeVazio.Nome);
            Assert.Equal("", clienteEmailVazio.Email);
            Assert.Equal("", clienteSenhaVazia.Password);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void Propriedades_PodemSerAlteradas(){
            
            // Arrange
            var cliente = new Cliente("A", "a@a.com", "x");
            const string novoNome = "Maria";
            const string novoEmail = "maria@email.com";
            const string novaSenha = "novaSenha";
            var dataAlteracao = DateTime.UtcNow;

            // Act
            cliente.Nome = novoNome;
            cliente.Email = novoEmail;
            cliente.Password = novaSenha;
            cliente.DataUltimaAlteracao = dataAlteracao;

            // Assert
            Assert.Equal(novoNome, cliente.Nome);
            Assert.Equal(novoEmail, cliente.Email);
            Assert.Equal(novaSenha, cliente.Password);
            Assert.Equal(dataAlteracao, cliente.DataUltimaAlteracao);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void Propriedades_PodemSerAlteradasParaValoresInvalidos(){
            // Arrange
            var cliente = new Cliente("A", "a@a.com", "x"){
                // Act
                Nome = null!,
                Email = "",
                Password = null!
            };

            // Assert
            Assert.Null(cliente.Nome);
            Assert.Equal("", cliente.Email);
            Assert.Null(cliente.Password);
        }

        [Fact]
        [Trait("Category", "UnitTest")]
        public void Contatos_DevePermitirAdicionarContato(){
            // Arrange
            var cliente = new Cliente("Teste", "t@t.com", "123");
            var contato = new Contato("Contato1", "c1@c.com", 11, "999999999");

            // Act
            cliente.Contatos.Add(contato);

            // Assert
            Assert.Single(cliente.Contatos);
            Assert.Equal(contato, cliente.Contatos.First());
        }
    }
} 