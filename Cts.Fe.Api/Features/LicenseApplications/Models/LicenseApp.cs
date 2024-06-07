namespace Cts.Fe.Features.LicenseApplications.Models;

/// <summary>
/// This is copied from decompiled code from CTSDBContext.
/// </summary>
public class LicenseApp
{
    public Guid Id { get; set; }

    public bool? IsSuspended { get; set; }

    public string OriginatorName { get; set; }

    public string Applicant { get; set; }
    
    public string? Assigendtoname { get; set; }
}