using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using NextIT_RomanM.Application.Extensions;
using NextIT_RomanM.Application.Filters;
using NextIT_RomanM.Core.Configuration;
using NextIT_RomanM.Core.Domain.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Authentication
// NOTICE: Injecting IOptions with Settings
builder.Services.Configure<UserSettings>(builder.Configuration.GetSection("User"));

// Exception handling
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddServicesAndRepositories();

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

app.UseCors(options =>
{
    options.SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
