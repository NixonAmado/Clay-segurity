using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        //CreateMap<Role, RoleDto>().ReverseMap();
        //CreateMap<PersonType, TypePDto>().ReverseMap();
        // CreateMap<Pet, FullPetDto>().ReverseMap();
                  //.ForMember(e => e.Species, op => op.MapFrom(e => e.Species.Name))
        CreateMap<G_CategoriaPersonaDto, CategoriaPersona>().ReverseMap();
        CreateMap<G_CiudadDto, Ciudad>().ReverseMap();
        CreateMap<G_contactoPersonaDto, ContactoPersona>().ReverseMap();
        CreateMap<G_contratoDto, Contrato>().ReverseMap();
        CreateMap<G_DepartamentoDto, Departamento>().ReverseMap();
        CreateMap<G_DireccionDto, Direccion>().ReverseMap();
        CreateMap<G_EstadoDto, Estado>().ReverseMap();
        CreateMap<G_PaisDto, Pais>().ReverseMap();
        CreateMap<G_PersonaDto, Persona>().ReverseMap();
        CreateMap<G_ProgramacionDto, Programacion>().ReverseMap();
        CreateMap<G_TipoContactoDto, TipoContacto>().ReverseMap();
        CreateMap<G_TipoDireccionDto, TipoDireccion>().ReverseMap();
        CreateMap<G_TipoPersonaDto, TipoPersona>().ReverseMap();
        CreateMap<PersonaNumerosDto, Persona>().ReverseMap();


        CreateMap<G_TurnoDto, Turno>().ReverseMap();
        CreateMap<Persona, TPersonaDto>()
           .ForMember(e => e.TipoPersona, op => op.MapFrom(e => e.TPersonaNavigation.Descripcion)).ReverseMap();

        CreateMap<Contrato, ContratoEstadoDto>()
           .ForMember(e => e.Cliente, op => op.MapFrom(e => e.ClienteNavigation.Nombre))
           .ForMember(e => e.Empleado, op => op.MapFrom(e => e.EmpleadoNavigation.Nombre))
           .ReverseMap();













    }
}