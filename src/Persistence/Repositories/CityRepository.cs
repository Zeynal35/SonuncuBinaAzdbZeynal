using Application.Abstracts.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories;

public class CityRepository:GenericRepository<City,int>,ICityRepository
{
    public CityRepository(BinaLiteDbContext context) : base(context)
    {
    }
}
