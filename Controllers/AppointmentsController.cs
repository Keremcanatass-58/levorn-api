using Levorn.Api.Data;
using Levorn.Api.DTOs;
using Levorn.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levorn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly LevornDbContext _context;

    public AppointmentsController(LevornDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentDto dto)
    {
        var customer = await _context.Customers.FindAsync(dto.CustomerId);

        if (customer == null)
        {
            return NotFound(new
            {
                message = "Customer not found"
            });
        }

        if (dto.EndTime <= dto.StartTime)
        {
            return BadRequest(new
            {
                message = "End time must be after start time."
            });
        }

        var hasConflict = await _context.Appointments
            .AnyAsync(a =>
                dto.StartTime < a.EndTime &&
                dto.EndTime > a.StartTime);

        if (hasConflict)
        {
            return Conflict(new
            {
                message = "Selected time slot is already booked."
            });
        }

        var appointment = new Appointment
        {
            CustomerId = dto.CustomerId,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Status = dto.Status,
            Notes = dto.Notes
        };

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(appointment);
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _context.Appointments
            .Include(a => a.Customer)
            .ToListAsync();

        return Ok(appointments);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _context.Appointments
            .Include(a => a.Customer)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (appointment == null)
        {
            return NotFound(new
            {
                message = "Appointment not found"
            });
        }

        return Ok(appointment);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateAppointmentDto dto)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound(new
            {
                message = "Appointment not found"
            });
        }

        var customer = await _context.Customers.FindAsync(dto.CustomerId);

        if (customer == null)
        {
            return NotFound(new
            {
                message = "Customer not found"
            });
        }

        appointment.CustomerId = dto.CustomerId;
        appointment.StartTime = dto.StartTime;
        appointment.EndTime = dto.EndTime;
        appointment.Status = dto.Status;
        appointment.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return Ok(appointment);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
        {
            return NotFound(new
            {
                message = "Appointment not found"
            });
        }

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Appointment deleted successfully"
        });
    }
}