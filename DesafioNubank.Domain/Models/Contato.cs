namespace DesafioNubank.Domain.Models;

public class Contato(string nome, string email, int ddd , string telefone){
    public Guid Id { get; set; } = Guid.CreateVersion7(); // Será a chave primária por convenção
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email; 
    public int Ddd { get; set; } = ddd; 
    public string Telefone { get; set; } = telefone; 
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;

    // Chave estrangeira para Cliente
    public Guid IdCliente { get; set; }

    // Propriedade de navegação para Cliente
    public virtual Cliente Cliente { get; set; }
}