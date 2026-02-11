using Application.Dtos.PropertyAd;
using Application.DTOs.PropertyAd;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class PropertyAdProfile : Profile
{
    public PropertyAdProfile()
    {
        // PropertyMedia -> PropertyMediaItemDto
        CreateMap<PropertyMedia, CreatePropertyMediaDto>();

        // PropertyAd -> GetAllPropertyAdResponse
        CreateMap<PropertyAd, GetAllPropertyAdResponse>()
            .ForMember(
                dest => dest.FirstMediaKey,
                opt => opt.MapFrom(src =>
                    src.PropertyMedias
                        .OrderBy(x => x.Order)
                        .Select(x => x.ObjectKey)
                        .FirstOrDefault()
                ));

        // PropertyAd -> GetByIdPropertyAdResponse
        CreateMap<PropertyAd, GetByIdPropertyAdResponse>()
            .ForMember(
                dest => dest.FirstMediaKey,
                opt => opt.MapFrom(src =>
                    src.PropertyMedias
                        .OrderBy(x => x.Order)
                        .Select(x => x.ObjectKey)
                        .FirstOrDefault()
                ))
            .ForMember(
                dest => dest.Media,
                opt => opt.MapFrom(src =>
                    src.PropertyMedias
                        .OrderBy(x => x.Order)
                ));

        // Create
        CreateMap<CreatePropertyAdRequest, PropertyAd>();
    }
}
