using AutoMapper;
using Challenge.Business.Features.Walk.Create;
using Challenge.Business.Features.Walk.Update;

namespace Challenge.Business.Mappings;

/// <summary>
/// Perfil de AutoMapper para mapeos relacionados con Walk
/// </summary>
public class WalkMappingProfile : Profile
{
    public WalkMappingProfile()
    {
        // Command → Entity
        CreateMap<CreateWalkCommand, Data.Entities.Walk>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Dog, opt => opt.Ignore())
            .ForMember(dest => dest.WalkedByUser, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());

        CreateMap<UpdateWalkCommand, Data.Entities.Walk>()
            .ForMember(dest => dest.Dog, opt => opt.Ignore())
            .ForMember(dest => dest.WalkedByUser, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore());
    }
}
