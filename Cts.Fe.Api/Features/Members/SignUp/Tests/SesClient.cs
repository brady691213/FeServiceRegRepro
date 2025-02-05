﻿using Amazon.SimpleEmailV2.Model;
using Cts.Fe.Tests;
using Cts.Fe.Tools;

namespace Cts.Fe.Features.Members.SignUp.Tests;

public sealed class SesClient : FakeSesClient
{
    public string? EmailMsg { get; set; }

    public override Task<SendEmailResponse> SendEmailAsync(SendEmailRequest request, CancellationToken ct = default)
    {
        EmailMsg = request.Content.Simple.Body.Html.Data;

        return Task.FromResult(new SendEmailResponse());
    }

    internal async Task<bool> EmailReceived(string memberId)
    {
        var ct = new CancellationTokenSource(TimeSpan.FromSeconds(5));

        while (!ct.IsCancellationRequested && EmailMsg.HasNoValue())
            await Task.Delay(250);

        return EmailMsg?.Contains(memberId) is true;
    }
}