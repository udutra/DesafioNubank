using DesafioNubank.Application.DTO.Response.Contato;

namespace DesafioNubank.Application.DTO.Response.Cliente;

public record ClienteResponseDto(Guid Id, string Nome, DateTime DataCadastro, ICollection<ContatoResponseDto> Contatos);