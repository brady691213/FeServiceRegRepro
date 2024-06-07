// ReSharper disable once RedundantUsingDirective
using Riok.Mapperly.Abstractions;

namespace Cts.Fe.Features.LicenseApplications.Models;

[Mapper]
[RegisterService<LicenseAppDomainMapper>(LifeTime.Scoped)]
public partial class LicenseAppDomainMapper
{
    [MapProperty(nameof(LicenseApplicationIndex.Assigendtoname), nameof(LicenseAppListItem.AssignedToName))]
    public partial List<LicenseAppListItem> EntityToListItem(List<LicenseApplicationIndex> list);
}