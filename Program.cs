using Microsoft.EntityFrameworkCore;
using WordPopularity.Abstractions;
using WordPopularity.Entities;
using WordPopularity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(callBack =>
{
    callBack.UseNpgsql(builder.Configuration.GetConnectionString("Baza"));
});

builder.Services.AddScoped<IWordPopularityService, WordPopularityService>();

var app = builder.Build();

app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.Run();


