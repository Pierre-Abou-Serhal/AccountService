using BAL;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
// Add services to the container.

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

if (environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
}

builder.Services.Configure<AppSetting>(builder.Configuration.GetSection("AppSetting"));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<AccountServiceBAL>();

var app = builder.Build();

if (!environment.IsDevelopment()) 
{
    // Bind to the PORT environment variable required by Heroku
    string port = Environment.GetEnvironmentVariable("PORT") ?? "8085"; // Default to 8085 if PORT not set
    app.Urls.Add($"http://*:{port}");
}

// Configure the HTTP request pipeline.
app.MapOpenApi();
app.MapScalarApiReference(options => { options.WithTitle("Account Service API"); });

app.UseCors(options => options
    .AllowAnyOrigin()    
    .AllowAnyMethod()
    .AllowAnyHeader()   
);

app.UseAuthorization();

app.MapControllers();

app.Run();