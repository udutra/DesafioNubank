using AutoMapper;
using DesafioNubank.Application.DTO.Request.Cliente;
using DesafioNubank.Application.DTO.Request.Contato;
using DesafioNubank.Application.DTO.Response.Cliente;
using DesafioNubank.Application.DTO.Response.Contato;
using DesafioNubank.Domain.Models;

namespace DesafioNubank.Application;

public class MappingProfile : Profile{
    public MappingProfile(){
        // Mapeamento para Cliente
        CreateMap<ClienteCreateDto, Cliente>();
        CreateMap<ClienteUpdateDto, Cliente>();
        CreateMap<Cliente, ClienteResponseDto>();

        // Mapeamento para Contato
        CreateMap<ContatoCreateDto, Contato>();
        CreateMap<ContatoUpdateDto, Contato>();
        CreateMap<Contato, ContatoResponseDto>();
    }
}