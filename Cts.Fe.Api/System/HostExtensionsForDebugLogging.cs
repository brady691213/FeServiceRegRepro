using System.Globalization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;
using Serilog.Formatting.Compact;

namespace Cts.Fe.System;

public static class HostExtensionsForDebugLogging
{
    public static void ConfigureSerilogForDiagnostics(this IHostBuilder host)
        => host.UseSerilog(
            (ctx, lc) => lc
                         .MinimumLevel.Verbose()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                         .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                         .MinimumLevel.Override("Microsoft.AspNetCore.Diagnostics.ExceptionHandlerMiddleware", LogEventLevel.Fatal)
                         .Enrich.WithProperty("ApplicationName", ctx.HostingEnvironment.ApplicationName)
                         .Enrich.FromLogContext()
                         .Enrich.WithEnvironmentName()
                         .Enrich.WithExceptionDetails(
                             new DestructuringOptionsBuilder()
                                 .WithDefaultDestructurers()
                                 .WithDestructurers(
                                 [
                                     new SqlExceptionDestructurer(),
                                 ]))
                         .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture)
                         .WriteTo.Seq(
                             serverUrl: "http://localhost:5341",
                             apiKey: "awz1rXJhgYM2xxKKYaZ2",
                             payloadFormatter: new RenderedCompactJsonFormatter()),
            preserveStaticLogger: true);
}