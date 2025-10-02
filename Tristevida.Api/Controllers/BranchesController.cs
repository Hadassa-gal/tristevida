using System;
using AutoMapper;
using CleanShop.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tristevida.Api.DTOs.Branches;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class BranchesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public BranchesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<BranchesDto>>> GetAll(CancellationToken ct)
    {
        var branches = await _unitofwork.Branches.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<BranchesDto>>(branches);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [DisableRateLimiting]
    public async Task<ActionResult<BranchesDto>> GetById(int id, CancellationToken ct)
    {
        var branch = await _unitofwork.Branches.GetByIdAsync(id, ct);
        if (branch is null) return NotFound();

        return Ok(_mapper.Map<BranchesDto>(branch));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBranchesDto body, CancellationToken ct)
    {
        var branch = _mapper.Map<Branches>(body);
        await _unitofwork.Branches.AddAsync(branch, ct);

        var dto = _mapper.Map<BranchesDto>(branch);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBranchesDto body, CancellationToken ct)
    {
        var branch = await _unitofwork.Branches.GetByIdAsync(id, ct);
        if (branch is null) return NotFound();

        _mapper.Map(body, branch);
        _unitofwork.Branches.UpdateAsync(branch, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var branch = await _unitofwork.Branches.GetByIdAsync(id, ct);
        if (branch is null) return NotFound();

        _unitofwork.Branches.DeleteAsync(id, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }
}
