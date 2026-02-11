using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.PropertyAd;
using AutoMapper;
using Domain.Entities;
using FluentValidation;

namespace Persistence.Services;

public class PropertyAdServices : IPropertyAdService
{
    private readonly IRepository<PropertyAd, int> _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreatePropertyAdRequest> _createValidator;

    public PropertyAdServices(IRepository<PropertyAd, int> repository, IMapper mapper, IValidator<CreatePropertyAdRequest> createValidator)
    {
        _repository = repository;
        _mapper = mapper;
        _createValidator = createValidator;
    }

    public async Task<bool> CreateAsync(CreatePropertyAdRequest request)
    {
        var validationResult = await _createValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        var entity = _mapper.Map<PropertyAd>(request);
        await _repository.AddAsync(entity);
        await _repository.SaveChanges();
        return true;
    }

    public async Task<List<GetAllPropertyAdResponse>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return _mapper.Map<List<GetAllPropertyAdResponse>>(entities);
    }

    public async Task<GetByIdPropertyAdResponse> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
            throw new KeyNotFoundException("PropertyAd tapilmadi");

        return _mapper.Map<GetByIdPropertyAdResponse>(entity);
    }


    public async Task<bool> UpdateAsync(int id, UpdatePropertyAdRequest request)
    {
        var propertyAd = await _repository.GetByIdAsync(id);

        if (propertyAd == null)
            throw new KeyNotFoundException("PropertyAd tapilmadi");

        propertyAd.Title = request.Title;
        propertyAd.Description = request.Description;
        propertyAd.Price = request.Price;

        await _repository.UpdateAsync(propertyAd);
        await _repository.SaveChanges();

        return true;
    }

}