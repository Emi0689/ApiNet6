using Microsoft.EntityFrameworkCore;
using ApiNet6.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbapiContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("StringDBAPI")));

builder.Services.AddControllers().AddJsonOptions(opt =>
    { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
);

var corsRules = "CorRules";
builder.Services.AddCors(opt =>
{ 
    opt.AddPolicy(name: corsRules,
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader()); 
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(corsRules);

app.UseAuthorization();

app.MapControllers();

app.Run();
