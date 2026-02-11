using Application.DTOs.PropertyAd;

namespace Application.Abstracts.Services;

public interface IPropertyAdService
{
    Task<List<GetAllPropertyAdResponse>> GetAllAsync();
    Task<GetByIdPropertyAdResponse> GetByIdAsync(int id);
    Task<bool> CreateAsync(CreatePropertyAdRequest request);
    Task<bool> UpdateAsync(int id, UpdatePropertyAdRequest request);
}
