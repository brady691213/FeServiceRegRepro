using Cts.Fe.Features.LicenseApplications.Models;

namespace Cts.Fe.Features.LicenseApplications;

public sealed class ListAllResponse: List<LicenseAppListItem>
{
    public ListAllResponse()
    {
    }

    public ListAllResponse(IEnumerable<LicenseAppListItem> items) : base(items)
    {
    }
}