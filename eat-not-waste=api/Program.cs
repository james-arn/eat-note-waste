using Serilog;
using eat_not_waste_api.Data;
using Microsoft.EntityFrameworkCore;
using eat_not_waste_api.Services;
using AutoMapper;
using eat_not_waste_api.Mappings;

var builder = WebApplication.CreateBuilder(args);

// Configure log file
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("log/eat-not-waste-Logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection"));
});

builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ListingService>();
builder.Services.AddScoped<MerchantService>();
builder.Services.AddScoped<PurchaseService>();
builder.Services.AddScoped<ReviewService>();

builder.Services.AddControllers(option => {
    // option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson();

// AutoMapper Configuration
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
