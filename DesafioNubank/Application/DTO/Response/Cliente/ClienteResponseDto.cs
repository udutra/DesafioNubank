using DesafioNubank.Api.Application.DTO.Response.Contato;

namespace DesafioNubank.Api.Application.DTO.Response.Cliente;

public record ClienteResponseDto(Guid Id, string Nome, DateTime DataCadastro, List<ContatoResponseDto> Contatos);