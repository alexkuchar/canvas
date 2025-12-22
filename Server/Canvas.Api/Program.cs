using Canvas.Api.Data;
using Canvas.Api.Extensions;
using Canvas.Api.Extensions.Authentication;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerDocs();
builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocs();
}

app.UseHttpsRedirection();
app.UseAuthenticationAndAuthorization();
app.MapControllers();

app.Run();