using Microsoft.EntityFrameworkCore;
using Svadba;
using Svadba.DBModel;
using Svadba.Models;
using Svadba.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddDbContext<PostgresDbContext>(
    (DbContextOptionsBuilder options) => options.UseNpgsql(connectionString)
    );

builder.Services.AddScoped<ExcelService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:8000", "https://front.marina-and-maxin-wedding-020825.ru/")
                .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors();

app.MapPost("/forma", async (FormaDTO model, PostgresDbContext context) =>
{
    var forma = new Forma()
    {
        Name = model.Name,
        Email = model.Email,
        Comment = model.Comment,
    };
    context.Formas.Add(forma);
    await context.SaveChangesAsync();
});
app.MapGet("/excel", async (ExcelService service) =>
{
    var fileData = await service.GetExcelAsync();
    return Results.File(fileData, "text/csv", "peoples.csv");
});

app.Run();

