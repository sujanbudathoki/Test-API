using Microsoft.EntityFrameworkCore;
using test_API.Data;
using test_API.Data.Repository;
using test_API.Domain.Repository;
using test_API.Service.IServices;
using test_API.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Fetch connection string and configure DbContext.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseSqlServer(connectionString));

//Register for DI
builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
builder.Services.AddScoped<ICandidateService, CandidateService>();

// Add services to the container.
builder.Services.AddControllers();
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
