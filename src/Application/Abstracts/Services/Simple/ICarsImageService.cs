using Application.DTOs.Simple;
using Application.DTOs.Street;

namespace Application.Abstracts.Services.Simple;

public interface ICarsImageService
{
    Task<List<GetAllCarsImageResponse>> GetAllAsync();
    Task CreateAsync(CreateCarsImageRequest request);
}
