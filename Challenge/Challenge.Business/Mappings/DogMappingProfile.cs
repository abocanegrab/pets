using AutoMapper;
using Challenge.Business.Features.Dog.Create;
using Challenge.Business.Features.Dog.Update;

namespace Challenge.Business.Mappings;

/// <summary>
/// Perfil de AutoMapper para mapeos relacionados con Dog
/// </summary>
public class DogMappingProfile : Profile
{
    public DogMappingProfile()
    {
        // Command → Entity
        CreateMap<CreateDogCommand, Data.Entities.Dog>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Walks, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());

        CreateMap<UpdateDogCommand, Data.Entities.Dog>()
            .ForMember(dest => dest.Client, opt => opt.Ignore())
            .ForMember(dest => dest.Walks, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
    }
}
