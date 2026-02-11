using Application.Abstracts.Repositories.SimpleRepo;
using Domain.Entities.Simple;
using Persistence.Context;

namespace Persistence.Repositories.Simple;

public class CarsImageRepository : GenericRepository<CarsImage, int>, ICarsImageRepo
{
    public CarsImageRepository(BinaLiteDbContext context) : base(context)
    {
    }
}
