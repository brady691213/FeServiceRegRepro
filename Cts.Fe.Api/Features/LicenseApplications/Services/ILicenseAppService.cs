using Ardalis.Result;

namespace Cts.Fe.Features.LicenseApplications;

public interface ILicenseAppService
{
    Task<Result<ListAllResponse>> ListAllAsync();
}