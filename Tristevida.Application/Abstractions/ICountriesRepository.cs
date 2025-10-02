using System;
using Tristevida.Domain.Entities;

namespace Tristevida.Application.Abstractions;

public interface ICountriesRepository
{
    Task<Countries> GetByIdAsync(int id, CancellationToken ct = default);
    Task<IEnumerable<Countries>> GetAllAsync(CancellationToken ct = default);
    Task<Countries> GetByNameAsync(string name, CancellationToken ct = default);
    Task AddAsync(Countries country, CancellationToken ct = default);
    Task UpdateAsync(Countries country, CancellationToken ct = default);
    Task DeleteAsync(int id, CancellationToken ct = default);
    Task<int> CountAsync(string? q, CancellationToken ct = default);
}
