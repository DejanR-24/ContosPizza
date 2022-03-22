using AutoMapper;
using ContoPizzaApi.Models;

namespace ContoPizzaApi.Services;

public class SaveMetadataBeverage:Profile
{
    public SaveMetadataBeverage()
    {
        CreateMap<Beverage, Metadata>()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
    .ForMember(dest => dest.Description, opt => opt.MapFrom(src => "This beverage containes alcohol: " + src.ContainsAlcohol))
    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Today));
    }

}
