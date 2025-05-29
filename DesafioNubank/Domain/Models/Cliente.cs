namespace DesafioNubank.Api.Domain.Models;

public class Cliente(string nome){
    public Guid Id { get; set; } = Guid.CreateVersion7(); // Será a chave primária por convenção
    public string Nome { get; set; } = nome;
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}