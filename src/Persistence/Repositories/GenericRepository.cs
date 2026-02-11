using Application.Abstracts.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence.Repositories;

public class GenericRepository<Tentity, Tkey> : IRepository<Tentity, Tkey> where Tentity : BaseEntity<Tkey>
{
    private readonly BinaLiteDbContext _context;
    private readonly DbSet<Tentity> _table;
    public GenericRepository(BinaLiteDbContext context)
    {
        _context = context;
        _table = _context.Set<Tentity>();
    }


    public async Task AddAsync(Tentity entity)
    {
        await _table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tentity entity)
    {
       _table.Remove(entity);
         await _context.SaveChangesAsync();
    }

    public async Task<List<Tentity>> GetAllAsync()
    {
      return await _table.ToListAsync();
    }

    public async Task<Tentity> GetByIdAsync(Tkey id)
    {
       
       var entity = await _table.FindAsync(id);
       if (entity == null)
           throw new InvalidOperationException($"Entity of type {typeof(Tentity).Name} with id '{id}' was not found.");
       return entity;
    }

    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Tentity entity)
    {
        _table.Update(entity);
        await _context.SaveChangesAsync();
    }
}
