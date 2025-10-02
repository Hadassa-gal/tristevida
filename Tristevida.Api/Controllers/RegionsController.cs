using System;
using AutoMapper;
using CleanShop.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tristevida.Api.DTOs.Regions;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class RegionsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public RegionsController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<RegionsDto>>> GetAll(CancellationToken ct)
    {
        var regions = await _unitofwork.Regions.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<RegionsDto>>(regions);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [DisableRateLimiting]
    public async Task<ActionResult<RegionsDto>> GetById(int id, CancellationToken ct)
    {
        var region = await _unitofwork.Regions.GetByIdAsync(id, ct);
        if (region is null) return NotFound();

        return Ok(_mapper.Map<RegionsDto>(region));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRegionDto body, CancellationToken ct)
    {
        var region = _mapper.Map<Regions>(body);
        await _unitofwork.Regions.AddAsync(region, ct);
        await _unitofwork.SaveChangesAsync(ct);
        
        var dto = _mapper.Map<RegionsDto>(region);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRegionDto body, CancellationToken ct)
    {
        var region = await _unitofwork.Regions.GetByIdAsync(id, ct);
        if (region is null) return NotFound();

        _mapper.Map(body, region);
        _unitofwork.Regions.UpdateAsync(region, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var region = await _unitofwork.Regions.GetByIdAsync(id, ct);
        if (region is null) return NotFound();

        _unitofwork.Regions.DeleteAsync(id, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }
}
