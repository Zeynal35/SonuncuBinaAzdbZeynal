using Application.Dtos.PropertyAd;
using Domain.Enums;

namespace Application.DTOs.PropertyAd;

public class GetByIdPropertyAdResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public double Price { get; set; }
    public string Location { get; set; } = null!;
    public int RoomCount { get; set; }
    public double Area { get; set; }
    public bool IsFurnished { get; set; }
    public bool IsMortgage { get; set; }
    public bool IsExtract { get; set; }
    public OfferType OfferType { get; set; }
    public RealEstateType RealEstateType { get; set; }

    // ƏLAVƏ (profile üçün məcburidir)
    public string? FirstMediaKey { get; set; }
    public List<CreatePropertyMediaDto> Media { get; set; } = new();
}

