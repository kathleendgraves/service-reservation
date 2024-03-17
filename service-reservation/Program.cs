using service_reservation.Managers;
using service_reservation.Managers.Interfaces;
using service_reservation.Repositories;
using service_reservation.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Managers
builder.Services.AddScoped<IScheduleManager, ScheduleManager>();
builder.Services.AddScoped<IAppointmentManager, AppointmentManager>();

// Repositories
builder.Services.AddSingleton<IScheduleRepository, ScheduleRepository>();
builder.Services.AddSingleton<IAppointmentRepository, AppointmentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
