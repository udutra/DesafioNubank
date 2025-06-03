namespace DesafioNubank.Application.DTO.Request.Cliente;

public class ClienteUpdateDto(string nome, string email, string password) : RequestBase {
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public DateTime DataUltimaAlteracao { get; set; } = DateTime.UtcNow;
}