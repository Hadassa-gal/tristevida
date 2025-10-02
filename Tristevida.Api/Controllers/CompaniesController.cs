using System;
using AutoMapper;
using CleanShop.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Tristevida.Api.DTOs.Companies;
using Tristevida.Application.Abstractions;
using Tristevida.Domain.Entities;
using Tristevida.Domain.ValueObjects;

namespace Tristevida.Api.Controllers;

[EnableRateLimiting("ipLimiter")]
public class CompaniesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitofwork;

    public CompaniesController(IMapper mapper, IUnitOfWork unitofwork)
    {
        _mapper = mapper;
        _unitofwork = unitofwork;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<CompaniesDto>>> GetAll(CancellationToken ct)
    {
        var companies = await _unitofwork.Companies.GetAllAsync(ct);
        var dto = _mapper.Map<IEnumerable<CompaniesDto>>(companies);
        return Ok(dto);
    }

    [HttpGet("{id:int}")]
    [DisableRateLimiting]
    public async Task<ActionResult<CompaniesDto>> GetById(int id, CancellationToken ct)
    {
        var company = await _unitofwork.Companies.GetByIdAsync(id, ct);
        if (company is null) return NotFound();

        return Ok(_mapper.Map<CompaniesDto>(company));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyDto body, CancellationToken ct)
    {
        var ukniu = Ukniu.Create(body.Ukniu);

        var company = new Companies(
            body.Name,
            ukniu,
            body.Address,
            body.Email,
            body.CityId
        );

        await _unitofwork.Companies.AddAsync(company, ct);

        var dto = _mapper.Map<CompaniesDto>(company);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCompanyDto body, CancellationToken ct)
    {
        var company = await _unitofwork.Companies.GetByIdAsync(id, ct);
        if (company is null) return NotFound();

        company.Name = body.Name;
        company.Ukniu = Ukniu.Create(body.Ukniu);
        company.Address = body.Address;
        company.Email = body.Email;
        company.CityId = body.CityId;

        _unitofwork.Companies.UpdateAsync(company, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var company = await _unitofwork.Companies.GetByIdAsync(id, ct);
        if (company is null) return NotFound();

        _unitofwork.Companies.DeleteAsync(id, ct);
        await _unitofwork.SaveChangesAsync(ct);

        return NoContent();
    }
}
