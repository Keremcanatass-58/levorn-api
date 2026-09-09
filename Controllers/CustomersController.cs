using Levorn.Api.Data;
using Levorn.Api.DTOs;
using Levorn.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Levorn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly LevornDbContext _context;

    public CustomersController(LevornDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerDto dto)
    {
        var customer = new Customer
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Phone = dto.Phone,
            Email = dto.Email,
            Notes = dto.Notes
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return Ok(customer);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();

        return Ok(customers);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound(new
            {
                message = "Customer not found"
            });
        }

        return Ok(customer);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateCustomerDto dto)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound(new
            {
                message = "Customer not found"
            });
        }

        customer.FirstName = dto.FirstName;
        customer.LastName = dto.LastName;
        customer.Phone = dto.Phone;
        customer.Email = dto.Email;
        customer.Notes = dto.Notes;

        await _context.SaveChangesAsync();

        return Ok(customer);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);

        if (customer == null)
        {
            return NotFound(new
            {
                message = "Customer not found"
            });
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Customer deleted successfully"
        });
    }
}