namespace Cts.Fe.Features.LicenseApplications;

public class LicenseAppGroup: Group
{
    public LicenseAppGroup()
    {
        Configure("/license-applications", _ =>{ });
    }
}