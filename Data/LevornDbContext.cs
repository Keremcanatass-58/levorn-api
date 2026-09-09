using Levorn.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Levorn.Api.Data;

public class LevornDbContext : DbContext
{
    public LevornDbContext(DbContextOptions<LevornDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
}