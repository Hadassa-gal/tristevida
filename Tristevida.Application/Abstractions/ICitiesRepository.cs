using System;
using Tristevida.Domain.Entities;

namespace Tristevida.Application.Abstractions;

public interface ICitiesRepository
{
    Task<Cities> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Cities>> GetAllAsync(CancellationToken ct = default);
    Task<Cities> GetByNameAsync(string name, CancellationToken ct = default);
    Task<IEnumerable<Cities>> GetByRegionIdAsync(int regionId, CancellationToken ct = default);
    Task AddAsync(Cities city, CancellationToken ct = default);
    Task UpdateAsync(Cities city, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
}
