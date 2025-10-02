using Microsoft.EntityFrameworkCore;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Repositories;

public class CountriesRepository(AppDbContext db) : ICountriesRepository
{
    public async Task<Countries?> GetByIdAsync(int id, CancellationToken ct = default)
        => await db.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IEnumerable<Countries>> GetAllAsync(CancellationToken ct = default)
        => await db.Countries.AsNoTracking().ToListAsync(ct);

    public async Task<Countries?> GetByNameAsync(string name, CancellationToken ct = default)
        => await db.Countries.AsNoTracking().FirstOrDefaultAsync(c => c.Name == name, ct);

    public Task AddAsync(Countries country, CancellationToken ct = default)
    {
        db.Countries.Add(country);
        return Task.CompletedTask; // Se confirma con UnitOfWork.SaveChangesAsync()
    }

    public Task UpdateAsync(Countries country, CancellationToken ct = default)
    {
        db.Countries.Update(country);
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var country = await db.Countries.FindAsync([id], ct);
        if (country is not null)
        {
            db.Countries.Remove(country);
            await Task.CompletedTask;
        }
    }

    public async Task<int> CountAsync(string? q, CancellationToken ct = default)
    {
        var query = db.Countries.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(q))
        {
            var term = q.Trim().ToUpper();
            query = query.Where(c => c.Name.ToUpper().Contains(term));
        }
        return await query.CountAsync(ct);
    }
}
