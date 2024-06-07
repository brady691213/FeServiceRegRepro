namespace Cts.Fe.Features.LicenseApplications.Models;

public record LicenseAppListItem
{
    public Guid Id { get; set; }
    
    public Guid AssignedTo { get; set; }
    
    public string? AssignedToName { get; set; }

    public DateTime? DateStarted { get; set; }

    public bool? IsLocked { get; set; }

    public string? OriginatorName { get; set; }

    public string? Applicant { get; set; }

    public string? IdNum { get; set; }
}