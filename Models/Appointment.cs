namespace Levorn.Api.Models;

public class Appointment
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public string Status { get; set; } = "Pending";

    public string? Notes { get; set; }

    public Customer Customer { get; set; } = null!;
}