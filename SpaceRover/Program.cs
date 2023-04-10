using SpaceRover;
using SpaceRover.ApplicationServices;
using SpaceRover.Configuration;
using SpaceRover.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<SpaceRoverConfiguration>()
    .BindConfiguration(Constants.ConfigurationSection, options => options.BindNonPublicProperties = true)
    .AddConfigurationValidations()
    .ValidateOnStart();

builder.Services.AddScoped<IRoverService, RoverService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();