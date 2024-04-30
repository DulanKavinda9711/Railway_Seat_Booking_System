using Microsoft.EntityFrameworkCore;
using TrainSchedule.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

string conn = "Data Source=DESKTOP-21SIMOF\\DULAN;Initial Catalog=Dulan62;User ID=sa;Password=sql123;Connect Timeout=30;Encrypt=False;TrustServerCertificate=Yes;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<TrainRepository>();
builder.Services.AddScoped<BookingRepository>();
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(conn));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost") // Specify the origin(s) from which you will allow requests
           .AllowAnyMethod() // Allow any HTTP method
           .AllowAnyHeader(); // Allow any HTTP header
});

app.UseAuthorization();

app.MapControllers();

app.Run();
