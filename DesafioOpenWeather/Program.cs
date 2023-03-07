using DesafioOpenWeather.DAO;
using DesafioOpenWeather.Database;
using DesafioOpenWeather.Util;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddSingleton<LocalContextDb>();
builder.Services.AddTransient<ClimaDAO>();
builder.Services.AddTransient<OpenWeatherService>();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
//builder.Services.AddLogging();
builder.Services.AddHostedService<TimerHostedService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Desafio OpenWeather", Version = "v1" });
    options.EnableAnnotations();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();
