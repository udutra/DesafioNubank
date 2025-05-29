namespace DesafioNubank.Api.Application.DTO.Response.Contato;

public record ContatoResponseDto(Guid Id, string Nome, string Email, int Ddd, string Telefone, DateTime DataCadastro, Guid IdCliente);