using Levorn.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<LevornDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseExceptionHandler("/error");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapGet("/error", () =>
{
    return Results.Problem(
        title: "An unexpected error occurred.",
        statusCode: StatusCodes.Status500InternalServerError
    );
});

app.MapGet("/", () =>
{
    return Results.Ok(new
    {
        name = "LEVORN API",
        status = "online",
        version = "1.0.0"
    });
});

app.Run();