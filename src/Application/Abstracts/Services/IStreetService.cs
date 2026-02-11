using Application.DTOs.Street;

namespace Application.Abstracts.Services;

public interface IStreetService
{
    Task<List<GetAllStreetResponse>> GetAllAsync();
    Task<GetByIdStreetResponse> GetByIdAsync(int id);
    Task<bool> CreateAsync(CreateStreetRequest request);
    Task<bool> UpdateAsync(int id, UpdateStreetRequest request);
}
