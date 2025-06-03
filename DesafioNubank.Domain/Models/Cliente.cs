namespace DesafioNubank.Domain.Models;

public class Cliente(string nome, string email, string password){
    public Guid Id { get; set; } = Guid.CreateVersion7(); // Será a chave primária por convenção
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
    public DateTime? DataUltimaAlteracao { get; set; }
    public virtual ICollection<Contato> Contatos { get; set; } = new List<Contato>();
}