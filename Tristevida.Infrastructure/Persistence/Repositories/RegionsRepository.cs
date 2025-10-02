using Microsoft.EntityFrameworkCore;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Repositories;

public class RegionsRepository(AppDbContext db) : IRegionsRepository
{
    public async Task<Regions?> GetByIdAsync(int id, CancellationToken ct = default)
        => await db.Regions.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, ct);

    public async Task<IEnumerable<Regions>> GetAllAsync(CancellationToken ct = default)
        => await db.Regions.AsNoTracking().ToListAsync(ct);

    public async Task<Regions?> GetByNameAsync(string name, CancellationToken ct = default)
        => await db.Regions.AsNoTracking().FirstOrDefaultAsync(r => r.Name == name, ct);

    public async Task<IEnumerable<Regions>> GetByCountryIdAsync(int countryId, CancellationToken ct = default)
        => await db.Regions.AsNoTracking().Where(r => r.CountryId == countryId).ToListAsync(ct);

    public Task AddAsync(Regions region, CancellationToken ct = default)
    {
        db.Regions.Add(region);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Regions region, CancellationToken ct = default)
    {
        db.Regions.Update(region);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var region = await db.Regions.FindAsync([id], ct);
        if (region is not null)
        {
            db.Regions.Remove(region);
            await Task.CompletedTask;
        }
    }

    public async Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = db.Regions.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(r => r.Name.ToUpper().Contains(term));
        }
        return await query.CountAsync(ct);
    }
}
