using System;
using Tristevida.Application.Abstractions;
using Tristevida.Infrastructure.Persistence;
using Tristevida.Infrastructure.Persistence.Repositories;

namespace Tristevida.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private ICompaniesRepository? _companiesRepository;
    private IBranchesRepository? _branchesRepository;
    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
    public async Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default)
    {
        await using var tx = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            await operation(ct);
            await _context.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }
    }
    public IBranchesRepository Branches => _branchesRepository ??= new BranchesRepository(_context);

    public ICompaniesRepository Companies => _companiesRepository ??= new CompaniesRepository(_context);

}

