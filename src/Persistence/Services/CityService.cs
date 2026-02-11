using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.City;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Persistence.Services;

public class CityService : ICityServices
{
    private readonly IRepository<City, int> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCityRequest> _createValidator;
    public CityService(IRepository<City, int> repository, IMapper mapper, IValidator<CreateCityRequest> createValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
    }

    public async Task<bool> CreateAsync(CreateCityRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var entity = _mapper.Map<City>(request);
        await _repository.AddAsync(entity);
        await _repository.SaveChanges();
        return true;
    }

    public async Task<List<GetAllCityResponse>> GetAllAsync()
    {
        var cities = await _repository.GetAllAsync();
        return _mapper.Map<List<GetAllCityResponse>>(cities);
    }

   public async Task<GetByIdCityResponse> GetByIdAsync(int id)
{
    var city = await _repository.GetByIdAsync(id);

    if (city == null)
        throw new KeyNotFoundException("City tapilmadi");

    return _mapper.Map<GetByIdCityResponse>(city);
}


   public async Task<bool> UpdateAsync(int id, UpdateCityRequest request)
{
    var entity = await _repository.GetByIdAsync(id);

    if (entity == null)
        throw new KeyNotFoundException("City tapilmadi");

    _mapper.Map(request, entity);

    await _repository.UpdateAsync(entity);
    await _repository.SaveChanges();

    return true;
}

}
