using FluentValidation;
using FluentValidation.AspNetCore;
using FourtitudeAsiaTest.BLL;
using FourtitudeAsiaTest.Loggers;
using FourtitudeAsiaTest.Middleware;
using FourtitudeAsiaTest.Model;
using FourtitudeAsiaTest.Validators;
using log4net.Config;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Disable built-in validation
});

builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

//Manually register FluentValidation and your validator
builder.Services.AddValidatorsFromAssemblyContaining<SubmitTransReqValidator>();
builder.Services.AddFluentValidationAutoValidation(); // Enables automatic validation

builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

//dependency
builder.Services.AddScoped<IItemBLL, ItemBLL>();

//appSettings
builder.Services.Configure<PartnerSettings>(builder.Configuration.GetSection("PartnerSettings"));

//log4net
XmlConfigurator.Configure(new FileInfo("Configs/log4net.config"));
builder.Services.AddSingleton(typeof(IAppLogger<>), typeof(Log4NetLogger<>));

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
