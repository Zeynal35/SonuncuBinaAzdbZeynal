using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services.Simple;
using Application.DTOs.Simple;
using Domain.Entities.Simple;
using System.ComponentModel.DataAnnotations;

namespace Persistence.Services;

public class CarsImageService : ICarsImageService
{
    private readonly ICarsImageRepo _carsImageRepo;

    public CarsImageService(ICarsImageRepo carsImageRepo)
    {
        _carsImageRepo = carsImageRepo;
    }

    public async Task CreateAsync(CreateCarsImageRequest request)
    {
        if (request.File == null || request.File.Length == 0)
            throw new ValidationException("File boş ola bilmez");


        var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        var fileName = Guid.NewGuid() + Path.GetExtension(request.File.FileName);
        var fullPath = Path.Combine(folderPath, fileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await request.File.CopyToAsync(stream);

        var entity = new CarsImage
        {
            FileName = request.File.FileName,
            FilePath = "/Uploads/" + fileName
        };

        await _carsImageRepo.AddAsync(entity);
        await _carsImageRepo.SaveChanges();
    }

    public async Task<List<GetAllCarsImageResponse>> GetAllAsync()
    {
        var entities = await _carsImageRepo.GetAllAsync();

        return entities.Select(e => new GetAllCarsImageResponse
        {
            Id = e.Id,
            FileName = e.FileName
        }).ToList();
    }
}
