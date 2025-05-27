namespace DesafioNubank.Api.Models;

public class Cliente(string nome, string email){
    public Guid Id { get; set; } = Guid.CreateVersion7(); // Será a chave primária por convenção
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email; 
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}