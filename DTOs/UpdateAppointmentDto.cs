using System.ComponentModel.DataAnnotations;

namespace Levorn.Api.DTOs;

public class UpdateAppointmentDto
{
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = "Pending";

    [StringLength(500)]
    public string? Notes { get; set; }
}