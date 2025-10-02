using System;
using AutoMapper;
using CleanShop.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tristevida.Api.DTOs.Countries;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class CountriesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public CountriesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CountriesDto>>> GetAll(CancellationToken ct)
    {
        var countries = await _unitofwork.Countries.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<CountriesDto>>(countries);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [DisableRateLimiting]
    public async Task<ActionResult<CountriesDto>> GetById(int id, CancellationToken ct)
    {
        var country = await _unitofwork.Countries.GetByIdAsync(id, ct);
        if (country is null) return NotFound();

        return Ok(_mapper.Map<CountriesDto>(country));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCountryDto body, CancellationToken ct)
    {
        var country = _mapper.Map<Countries>(body);
        await _unitofwork.Countries.AddAsync(country, ct);

        var dto = _mapper.Map<CountriesDto>(country);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryDto body, CancellationToken ct)
    {
        var country = await _unitofwork.Countries.GetByIdAsync(id, ct);
        if (country is null) return NotFound();

        _mapper.Map(body, country);
        _unitofwork.Countries.UpdateAsync(country, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var country = await _unitofwork.Countries.GetByIdAsync(id, ct);
        if (country is null) return NotFound();

        _unitofwork.Countries.DeleteAsync(id, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }
}
