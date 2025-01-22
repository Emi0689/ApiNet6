using Microsoft.EntityFrameworkCore;
using ApiNet6.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbapiContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLAZURECONNSTR_StringDBAPI")));

builder.Services.AddControllers().AddJsonOptions(opt =>
    { opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; }
);

var corsRules = "CorRules";
builder.Services.AddCors(opt =>
{ 
    opt.AddPolicy(name: corsRules,
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader()); 
});

builder.Configuration.AddJsonFile("appsettings.json");
var secretkey = builder.Configuration.GetSection("settings").GetSection("secretkey").ToString();
var keyBytes = Encoding.UTF8.GetBytes(secretkey);

builder.Services.AddAuthentication(config =>
{
    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(config => {
    config.RequireHttpsMetadata = false;
    config.SaveToken = true;
    config.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}


app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(corsRules);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
