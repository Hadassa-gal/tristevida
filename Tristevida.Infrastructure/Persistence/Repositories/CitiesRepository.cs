using Microsoft.EntityFrameworkCore;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Repositories;

public class CitiesRepository(AppDbContext db) : ICitiesRepository
{
    public async Task<Cities?> GetByIdAsync(int id, CancellationToken ct = default)
        => await db.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IEnumerable<Cities>> GetAllAsync(CancellationToken ct = default)
        => await db.Cities.AsNoTracking().ToListAsync(ct);

    public async Task<Cities?> GetByNameAsync(string name, CancellationToken ct = default)
        => await db.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name, ct);

    public async Task<IEnumerable<Cities>> GetByRegionIdAsync(int regionId, CancellationToken ct = default)
        => await db.Cities.AsNoTracking().Where(c => c.RegionId == regionId).ToListAsync(ct);

    public Task AddAsync(Cities city, CancellationToken ct = default)
    {
        db.Cities.Add(city);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Cities city, CancellationToken ct = default)
    {
        db.Cities.Update(city);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var city = await db.Cities.FindAsync([id], ct);
        if (city is not null)
        {
            db.Cities.Remove(city);
            await Task.CompletedTask;
        }
    }

    public async Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = db.Cities.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(c => c.Name.ToUpper().Contains(term));
        }
        return await query.CountAsync(ct);
    }
}
