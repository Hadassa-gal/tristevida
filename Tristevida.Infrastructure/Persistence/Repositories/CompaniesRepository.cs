using System;
using Microsoft.EntityFrameworkCore;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;
using Tristevida.Domain.ValueObjects;

namespace Tristevida.Infrastructure.Persistence.Repositories;

public sealed class CompaniesRepository(AppDbContext db) : ICompaniesRepository
{

    public async Task<Companies> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var company = await db.Companies.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id, ct);
        if (company == null)
            throw new KeyNotFoundException($"Company with Id '{id}' not found.");
        return company;
    }


    public async Task<Companies> GetByNameAsync(string name, CancellationToken ct = default)
    {
        var company = await db.Companies.FirstOrDefaultAsync(c => c.Name == name, ct);
        if (company == null)
            throw new KeyNotFoundException($"Company with Name '{name}' not found.");
        return company;
    }

    public Task<bool> ExistUkniuAsync(Ukniu ukniu, CancellationToken ct = default)
        => db.Companies.AnyAsync(c => c.Ukniu == ukniu, ct);

    public Task<Companies> GetByUkniuAsync(Ukniu ukniu, CancellationToken ct = default)
        => db.Companies.FirstOrDefaultAsync(c => c.Ukniu == ukniu, ct);

    public async Task AddAsync(Companies company, CancellationToken ct = default)
    {
        await db.Companies.AddAsync(company, ct);
    }

    public async Task UpdateAsync(Companies company, CancellationToken ct = default)
    {
        db.Companies.Update(company);
        await db.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Companies company, CancellationToken ct = default)
    {
        db.Companies.Remove(company);
        await db.SaveChangesAsync(ct);
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        var query = db.Companies.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(p =>
                p.Name.ToUpper().Contains(term) ||
                p.Ukniu.Value.ToUpper().Contains(term));
        }
        return query.CountAsync(ct);
    }

    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var company = await db.Companies.FindAsync([id], ct);
        if (company is not null)
        {
            db.Companies.Remove(company);
            await Task.CompletedTask;
        }
    }

    public async Task<IEnumerable<Companies>> GetAllAsync(CancellationToken ct = default)
        => await db.Companies.AsNoTracking().ToListAsync(ct);
}