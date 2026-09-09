using Levorn.Api.Data;
using Levorn.Api.DTOs;
using Levorn.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levorn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly LevornDbContext _context;

    public ServicesController(LevornDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceDto dto)
    {
        var service = new Service
        {
            Name = dto.Name,
            Description = dto.Description,
            DurationMinutes = dto.DurationMinutes,
            Price = dto.Price
        };

        _context.Services.Add(service);
        await _context.SaveChangesAsync();

        return Ok(service);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _context.Services
            .Where(s => s.IsActive)
            .ToListAsync();

        return Ok(services);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceDto dto)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
        {
            return NotFound(new
            {
                message = "Service not found"
            });
        }

        service.Name = dto.Name;
        service.Description = dto.Description;
        service.DurationMinutes = dto.DurationMinutes;
        service.Price = dto.Price;
        service.IsActive = dto.IsActive;

        await _context.SaveChangesAsync();

        return Ok(service);
    }
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> Deactivate(int id)
    {
        var service = await _context.Services.FindAsync(id);

        if (service == null)
        {
            return NotFound(new
            {
                message = "Service not found"
            });
        }

        service.IsActive = false;

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Service deactivated successfully."
        });
    }
}