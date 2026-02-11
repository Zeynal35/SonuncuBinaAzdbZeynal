using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.DTOs.Street;
using Domain.Entities;
using FluentValidation;

namespace Persistence.Services;

public class StreetService : IStreetService
{
    private readonly IRepository<Street, int> _repository;
    private readonly IValidator<CreateStreetRequest> _validator;

    public StreetService(IRepository<Street, int> repository,
                          IValidator<CreateStreetRequest> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<bool> CreateAsync(CreateStreetRequest request)
    {
        var validationResult = await _validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var street = new Street
        {
            Name = request.Name,
            Length = request.Length,
            Description = request.Description,
            CityId = request.CityId
        };

        await _repository.AddAsync(street);
        await _repository.SaveChanges();
        return true;
    }

    public async Task<List<GetAllStreetResponse>> GetAllAsync()
    {
        var streets = await _repository.GetAllAsync();

        return streets.Select(s => new GetAllStreetResponse
        {
            Id = s.Id,
            Name = s.Name,
            Length = s.Length,
            Description = s.Description,
            CityId = s.CityId
        }).ToList();
    }

    public async Task<GetByIdStreetResponse> GetByIdAsync(int id)
    {
        var street = await _repository.GetByIdAsync(id);

        if (street == null)
            throw new KeyNotFoundException("Street tapilmadi");

        return new GetByIdStreetResponse
        {
            Id = street.Id,
            Name = street.Name,
            Length = street.Length,
            Description = street.Description,
            CityId = street.CityId
        };
    }


    public async Task<bool> UpdateAsync(int id, UpdateStreetRequest request)
    {
        var street = await _repository.GetByIdAsync(id);

        if (street == null)
            throw new KeyNotFoundException("Street tapilmadi");

        street.Name = request.Name;
        street.Description = request.Description;

        await _repository.UpdateAsync(street);
        await _repository.SaveChanges();

        return true;
    }
}