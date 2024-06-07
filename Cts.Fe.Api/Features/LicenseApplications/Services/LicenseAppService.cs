using Ardalis.Result;
using Cts.Fe.Infrastructure;
using Cts.Fe.Tools;
using Microsoft.EntityFrameworkCore;
using LicenseAppDomainMapper = Cts.Fe.Features.LicenseApplications.Models.LicenseAppDomainMapper;

namespace Cts.Fe.Features.LicenseApplications;

[RegisterService<LicenseAppService>(LifeTime.Scoped)]
public class LicenseAppService : ILicenseAppService
{
    private readonly IConfiguration _configuration;
    private readonly ExternalDBContext _dbContext;
    private readonly LicenseAppDomainMapper _mapper;

    public LicenseAppService(IConfiguration configuration, ExternalDBContext dbContext, LicenseAppDomainMapper mapper)
    {
        _configuration = configuration;
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Result<ListAllResponse>> ListAllAsync()
    {
        var cs = _configuration.GetConnectionString(Constants.CtsLocalConnection);
        _dbContext.Database.SetConnectionString(cs);
        var entites = await _dbContext.LicenseApplicationIndices.ToListAsync();
        var items = _mapper.EntityToListItem(entites);
        return Result<ListAllResponse>.Success(new(items));
    }
}