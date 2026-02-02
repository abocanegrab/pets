using AutoMapper;
using Challenge.Business.Features.Client.Create;
using Challenge.Business.Features.Client.Update;

namespace Challenge.Business.Mappings;

/// <summary>
/// Perfil de AutoMapper para mapeos relacionados con Client
/// </summary>
public class ClientMappingProfile : Profile
{
    public ClientMappingProfile()
    {
        // Command → Entity
        CreateMap<CreateClientCommand, Data.Entities.Client>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Dogs, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.PersonType, opt => opt.Ignore());

        CreateMap<UpdateClientCommand, Data.Entities.Client>()
            .ForMember(dest => dest.Dogs, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.Ignore())
            .ForMember(dest => dest.PersonType, opt => opt.Ignore());
    }
}
