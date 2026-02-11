using Application.Abstracts.Repositories;
using Domain.Entities;
using Persistence.Context;

namespace Persistence.Repositories;

public class StreetRepository:GenericRepository<Street,int>, IStreetRepository
{
    public StreetRepository(BinaLiteDbContext context) : base(context)
    {
    }
}
