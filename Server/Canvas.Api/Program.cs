using Canvas.Api.Extensions;
using Canvas.Api.Middleware;
using Canvas.Application.Extensions;
using Canvas.Infrastructure.Extensions;
using Canvas.Infrastructure.Extensions.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApi();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseApi();
app.UseAuthenticationAndAuthorization();

app.Run();