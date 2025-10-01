using System;
using Tristevida.Domain.Entities;

namespace Tristevida.Application.Abstractions;

public interface IBranchesRepository
{
    Task<Branches> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Branches>> GetAllAsync(CancellationToken ct = default);
    Task<Branches> GetByNumber_ComercialAsync(int numberComercial, CancellationToken ct = default);
    Task<bool> ExistNumber_ComercialAsync(int numberComercial, CancellationToken ct = default);
    Task<Branches> GetByContactNameAsync(string contactName, CancellationToken ct = default);
    Task AddAsync(Branches branch, CancellationToken ct = default);
    Task UpdateAsync(Branches branch, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
}
