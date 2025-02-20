using Application.Services;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddInfrastructure();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
