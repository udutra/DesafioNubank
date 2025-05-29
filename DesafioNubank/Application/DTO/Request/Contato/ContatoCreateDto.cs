namespace DesafioNubank.Api.Application.DTO.Request.Contato;

public class ContatoCreateDto(string nome, string email, int ddd, string telefone, Guid idCliente) : RequestBase{
    public string Nome { get; set; } = nome;
    public string Email { get; set; } = email;
    public int Ddd { get; set; } = ddd;
    public string Telefone { get; set; } = telefone;
    private Guid IdCliente{ get; set; } = idCliente;
}