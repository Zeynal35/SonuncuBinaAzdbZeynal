using Domain.Entities;

namespace Application.Abstracts.Repositories;

public interface IRepository<Tentity,Tkey> where Tentity : BaseEntity<Tkey>
{
    Task<List<Tentity>> GetAllAsync();
    Task AddAsync(Tentity entity);
    Task UpdateAsync(Tentity entity);
    Task DeleteAsync(Tentity entity);
    Task<Tentity> GetByIdAsync(Tkey id);
    Task SaveChanges();
    
}
