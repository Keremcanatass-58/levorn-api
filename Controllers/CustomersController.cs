using Levorn.Api.DTOs;
using Levorn.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Levorn.Api.Data;
namespace Levorn.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
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
    private readonly LevornDbContext _context;

    public CustomersController(LevornDbContext context)
    {
        _context = context;
    }
}