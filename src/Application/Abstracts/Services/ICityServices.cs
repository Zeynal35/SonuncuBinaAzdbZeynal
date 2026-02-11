using Application.DTOs.City;
using Application.DTOs.PropertyAd;

namespace Application.Abstracts.Services;

public interface ICityServices
{
    Task<List<GetAllCityResponse>> GetAllAsync();
    Task<GetByIdCityResponse> GetByIdAsync(int id);
    Task<bool> CreateAsync(CreateCityRequest request);
    Task<bool> UpdateAsync(int id, UpdateCityRequest request);
}
