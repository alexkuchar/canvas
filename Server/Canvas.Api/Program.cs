using Canvas.Api.Extensions;
using Canvas.Api.Extensions.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerDocs();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();
}

app.UseHttpsRedirection();
app.UseAuthenticationAndAuthorization();
app.MapControllers();

app.Run();