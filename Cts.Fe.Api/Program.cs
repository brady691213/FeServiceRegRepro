using Cts.Fe.Infrastructure;
using Cts.Fe.System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ExternalDBContext>();
builder.Services.AddFastEndpoints();
if (!builder.Environment.IsProduction())
{
    builder.Services.AddSwaggerDocument();
}

var app = builder.Build();
app.UseDefaultExceptionHandler()
   .UseFastEndpoints(
       c =>
       {
           c.Errors.UseProblemDetails();
       });

if (!app.Environment.IsProduction())
    app.AddSwaggerGen();

app.Run();

namespace Cts.Fe
{
    public partial class Program;
}