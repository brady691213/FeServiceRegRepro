using System.Globalization;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;

namespace Cts.Fe.System;

public static class HostExtensionsForStandardLoggin
{
    public static void ConfigureSerilog(this IHostBuilder host)
        => host.UseSerilog(
            (_, lc) => lc
                         .MinimumLevel.Information()
                         .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                         .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                         .MinimumLevel.Override("System.Net.Http.HttpClient.Refit.Implementation", LogEventLevel.Warning)
                         .Enrich.FromLogContext()
                         .Enrich.WithEnvironmentName()
                         .Enrich.WithExceptionDetails(
                             new DestructuringOptionsBuilder()
                                 .WithDefaultDestructurers()
                                 .WithDestructurers(
                                 [
                                     new SqlExceptionDestructurer(),

                                     //new ApiExceptionDestructurer(),
                                 ]))
                         .WriteTo.Console(formatProvider: CultureInfo.InvariantCulture));
}