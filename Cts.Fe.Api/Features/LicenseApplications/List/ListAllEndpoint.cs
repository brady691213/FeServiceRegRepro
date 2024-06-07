using Ardalis.Result;
using Guard = CommunityToolkit.Diagnostics.Guard;

namespace Cts.Fe.Features.LicenseApplications;

sealed class ListAllEndpoint : Endpoint<ListAllRequest, Result<ListAllResponse>>
{
    private readonly LicenseAppService _licenseAppService;

    public ListAllEndpoint(LicenseAppService LicenseAppService)
    {
        _licenseAppService = LicenseAppService;
    }

    //public LicenseAppService LicenseAppService { get; set; }
    
    public override void Configure()
    {
        Get("/");
        Group<LicenseAppGroup>();
        PostProcessor<ListAllResponseSender>();
        AllowAnonymous();
        DontAutoSendResponse();            

    }

    public override async Task<Result<ListAllResponse>> ExecuteAsync(ListAllRequest r, CancellationToken ct)
    {
        Guard.IsNotNull(_licenseAppService, nameof(_licenseAppService));
        
        var appList = await _licenseAppService.ListAllAsync();
        return appList;
    }
}