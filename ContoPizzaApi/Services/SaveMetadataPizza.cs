using AutoMapper;
using ContoPizzaApi.Interfaces;
using ContoPizzaApi.Models;
using System.Text.Json;

namespace ContoPizzaApi.Services;

public class SaveMetadataPizza :Profile
{
   public SaveMetadataPizza()
    {
        CreateMap<Pizza,Metadata>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => "This pizza is gluten free: "+ src.IsGlutenFree))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Today));
    }


   
}
