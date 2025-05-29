namespace DesafioNubank.Api.Application.DTO.Request.Cliente;

public class ClienteCreateDto(string nome) : RequestBase{
    public string Nome { get; set; } = nome;
}