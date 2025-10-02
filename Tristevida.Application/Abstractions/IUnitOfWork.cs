using System;

namespace Tristevida.Application.Abstractions;

public interface IUnitOfWork
{
    IBranchesRepository Branches { get; }
    ICompaniesRepository Companies { get; }
    ICitiesRepository Cities { get; }
    IRegionsRepository Regions { get; }
    ICountriesRepository Countries { get; }
    // Task<int> SaveAsync();
    Task<int> SaveChangesAsync(CancellationToken ct = default);
    Task ExecuteInTransactionAsync(Func<CancellationToken, Task> operation, CancellationToken ct = default);
}