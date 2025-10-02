using System;
using Tristevida.Domain.Entities;

namespace Tristevida.Application.Abstractions;

public interface IRegionsRepository
{
    Task<Regions> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Regions>> GetAllAsync(CancellationToken ct = default);
    Task<Regions> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IEnumerable<Regions>> GetByCountryIdAsync(int countryId, CancellationToken ct = default);
    Task AddAsync(Regions region, CancellationToken ct = default);
    Task UpdateAsync(Regions region, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
}
