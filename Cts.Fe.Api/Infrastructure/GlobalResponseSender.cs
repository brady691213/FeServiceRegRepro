using Ardalis.Result;
using FluentValidation.Results;

namespace Cts.Fe.Infrastructure;

sealed class GlobalResponseSender : IGlobalPostProcessor
{
    public async Task PostProcessAsync(IPostProcessorContext context, CancellationToken ct)
    {
        if (!context.HttpContext.ResponseStarted())
        {
            var result = (IResult)Guard.Against.Null(context.Response, parameterName: nameof(context.Response));

            switch (result.Status)
            {
                case ResultStatus.Ok:
                    await context.HttpContext.Response.SendAsync(result.GetValue(), cancellation: ct);
                    break;

                // TASKT: Handle results that carry exception information here.
                case ResultStatus.Error:
                    await context.HttpContext.Response.SendOkAsync(result.GetValue(), cancellation: ct);
                    break;
                
                case ResultStatus.Invalid:
                    var failures = result.ValidationErrors.Select(e => new ValidationFailure(e.Identifier, e.ErrorMessage)).ToList();
                    await context.HttpContext.Response.SendErrorsAsync(failures, cancellation: ct);
                    break;
                default:
                    await context.HttpContext.Response.SendAsync(result.GetValue(), cancellation: ct);
                    break;
            }
        }

    }
}
