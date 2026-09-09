var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();

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