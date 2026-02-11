using Application.Dtos.PropertyAd;

public interface IPropertyMediaService
{
    Task<CreatePropertyMediaDto> CreateAsync(CreatePropertyMediaDto dto);
}


