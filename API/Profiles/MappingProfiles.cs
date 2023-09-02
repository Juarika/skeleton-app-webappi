using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pais, PaisDto>()
            .ReverseMap()
            .ForMember(m => m.Departamentos, d => d.Ignore());
        CreateMap<Departamento, DepartamentoDto>().ReverseMap();
        CreateMap<Pais, PaisxDepDto>().ReverseMap();
    }
}