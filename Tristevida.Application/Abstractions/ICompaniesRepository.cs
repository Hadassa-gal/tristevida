using System;
using Tristevida.Domain.Entities;
using Tristevida.Domain.ValueObjects;

namespace Tristevida.Application.Abstractions;

public interface ICompaniesRepository
{
    Task<Companies> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Companies>> GetAllAsync(CancellationToken ct = default);
    Task<Companies> GetByNameAsync(string name, CancellationToken ct = default);
    Task<bool> ExistUkniuAsync(Ukniu ukniu, CancellationToken ct = default);
    Task<Companies> GetByUkniuAsync(Ukniu ukniu, CancellationToken ct = default);
    Task AddAsync(Companies company, CancellationToken ct = default);
    Task UpdateAsync(Companies company, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
}
