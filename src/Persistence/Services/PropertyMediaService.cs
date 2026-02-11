using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Dtos.PropertyAd;
using Domain.Entities;

public class PropertyMediaService : IPropertyMediaService
{
    private readonly IRepository<PropertyMedia, int> _repository;
    private readonly IFileStorageService _fileStorage;

    public PropertyMediaService(
        IRepository<PropertyMedia, int> repository,
        IFileStorageService fileStorage)
    {
        _repository = repository;
        _fileStorage = fileStorage;
    }

    public async Task<CreatePropertyMediaDto> CreateAsync(CreatePropertyMediaDto dto)
    {
        var objectKey = await _fileStorage.SaveAsync(
            dto.File.OpenReadStream(),
            dto.File.FileName,
            dto.File.ContentType);

        var media = new PropertyMedia
        {
            ObjectKey = objectKey,
            Order = dto.Order,
            PropertyAdId = dto.PropertyAdId
        };

        _repository.AddAsync(media);
        _repository.SaveChanges();

        return new CreatePropertyMediaDto
        {
            Id = media.Id,
            ObjectKey = media.ObjectKey,
            Order = media.Order
        };
    }

  
}

