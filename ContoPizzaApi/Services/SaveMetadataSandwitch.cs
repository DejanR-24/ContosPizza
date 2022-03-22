using AutoMapper;
using ContoPizzaApi.Models;

namespace ContoPizzaApi.Services;

public class SaveMetadataSandwitch:Profile
{
    public SaveMetadataSandwitch()
    {
        CreateMap<Sandwitch, Metadata>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => "This sandwitch is with mayo: " + src.IsWithMayo))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Today));
    }
}
