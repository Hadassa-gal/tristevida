using System;
using Microsoft.EntityFrameworkCore;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Infrastructure.Persistence.Repositories;

public class BranchesRepository(AppDbContext db) : IBranchesRepository
{

    public async Task<IEnumerable<Branches>> GetAllAsync(CancellationToken ct = default)
        => await db.Branches.AsNoTracking().ToListAsync(ct);

    public async Task<Branches?> GetByContactNameAsync(string contactName, CancellationToken ct = default)
    {
        return await db.Branches.AsNoTracking().FirstOrDefaultAsync(b => b.Contact_Name == contactName, ct);
    }

    public async Task<Branches?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await db.Branches.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id, ct);
    }

    public async Task<Branches?> GetByNumber_ComercialAsync(int numberComercial, CancellationToken ct = default)
    {
        return await db.Branches.AsNoTracking()
                                .FirstOrDefaultAsync(b => b.Number_Comercial == numberComercial, ct);
    }
    public async Task AddAsync(Branches branch, CancellationToken ct = default)
    {
        db.Branches.Add(branch);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(Branches branch, CancellationToken ct = default)
    {
        db.Branches.Update(branch);
        await Task.CompletedTask;
    }
    public async Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var branch = await db.Branches.FindAsync([id], ct);
        if (branch is not null)
        {
            db.Branches.Remove(branch);
            await Task.CompletedTask;
        }
    }

    public Task<int> CountAsync(string? search = null, CancellationToken ct = default)
    {
        var query = db.Branches.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToUpper();
            query = query.Where(p =>
                p.Contact_Name.ToUpper().Contains(term));
        }
        return query.CountAsync(ct);
    }

    public Task<bool> ExistNumber_ComercialAsync(int numberComercial, CancellationToken ct = default)
    => db.Branches.AsNoTracking().AnyAsync(b => b.Number_Comercial == numberComercial, ct);

}
