using Cts.Fe.Features.LicenseApplications.Models;
using Microsoft.EntityFrameworkCore;

namespace Cts.Fe.Infrastructure;

/// <summary>
/// This is decompiled just for this repro from an externally developed libary I cannot include for NDA reasons. 
/// </summary>
public class ExternalDBContext: DbContext
{
    public virtual DbSet<LicenseApp> LicenseApplicationIndices { get; set; }

}