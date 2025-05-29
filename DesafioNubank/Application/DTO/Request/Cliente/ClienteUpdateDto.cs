namespace DesafioNubank.Api.Application.DTO.Request.Cliente;

public class ClienteUpdateDto(string nome) : RequestBase {
    public string Nome { get; set; } = nome;
}