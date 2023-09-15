
using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // CreateMap<Entity, EntityDto>().ReverseMap();
        CreateMap<Categoria, CategoriaDto>().ReverseMap();
        CreateMap<Chef, ChefDto>().ReverseMap();
        CreateMap<Ingrediente, IngredienteDto>().ReverseMap();
        CreateMap<Hamburguesa, HamburguesaDto>().ReverseMap();
        CreateMap<HamburguesaIngrediente, HamburguesaIngredienteDto>().ReverseMap();
    }
}
