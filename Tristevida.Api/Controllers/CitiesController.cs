using System;
using AutoMapper;
using CleanShop.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tristevida.Api.DTOs.Cities;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class CitiesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public CitiesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CitiesDto>>> GetAll(CancellationToken ct)
    {
        var cities = await _unitofwork.Cities.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<CitiesDto>>(cities);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [DisableRateLimiting]
    public async Task<ActionResult<CitiesDto>> GetById(int id, CancellationToken ct)
    {
        var city = await _unitofwork.Cities.GetByIdAsync(id, ct);
        if (city is null) return NotFound();

        return Ok(_mapper.Map<CitiesDto>(city));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCityDto body, CancellationToken ct)
    {
        var city = _mapper.Map<Cities>(body);
        await _unitofwork.Cities.AddAsync(city, ct);

        var dto = _mapper.Map<CitiesDto>(city);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCityDto body, CancellationToken ct)
    {
        var city = await _unitofwork.Cities.GetByIdAsync(id, ct);
        if (city is null) return NotFound();

        _mapper.Map(body, city);
        _unitofwork.Cities.UpdateAsync(city, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var city = await _unitofwork.Cities.GetByIdAsync(id, ct);
        if (city is null) return NotFound();

        _unitofwork.Cities.DeleteAsync(id, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }
}
