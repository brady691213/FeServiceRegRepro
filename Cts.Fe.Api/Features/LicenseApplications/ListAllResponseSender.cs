using Ardalis.Result;
using FluentValidation.Results;

namespace Cts.Fe.Features.LicenseApplications;

public class ListAllResponseSender : IPostProcessor<ListAllRequest, Result<ListAllResponse>>
{
    public async Task PostProcessAsync(IPostProcessorContext<ListAllRequest, Result<ListAllResponse>> context,
        CancellationToken ct)
    {
        if (!context.HttpContext.ResponseStarted())
        {
            var result = context.Response;
            //Guard.Against.Null(result, nameof(result));

            if (result is not null)
            {
                switch (result.Status)
                {
                    case ResultStatus.Ok:
                        await context.HttpContext.Response.SendAsync(result.GetValue(), cancellation: ct);
                        break;

                    case ResultStatus.Invalid:
                        var failures = result.ValidationErrors
                                             .Select(e => new ValidationFailure(e.Identifier, e.ErrorMessage)).ToList();
                        await context.HttpContext.Response.SendErrorsAsync(failures, cancellation: ct);
                        break;
                }
            }
        }
    }
}