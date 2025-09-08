using Microsoft.EntityFrameworkCore;
using autoschedular.Model;
using autoschedular.Services;
using autoschedular.Services.implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AutoSchedularDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register services
builder.Services.AddScoped<ICourseServices, CourseServices>();
builder.Services.AddScoped<IBatchService, BatchService>();
builder.Services.AddScoped<ILecturerService, LecturerService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IPoStaffService, PoStaffService>();
builder.Services.AddScoped<IModuleService, ModuleService>();
builder.Services.AddScoped<ILecturerTimetableService, LecturerTimetableService>();
builder.Services.AddScoped<IAssignModuleService, AssignModuleService>();
builder.Services.AddScoped<ITimeTableService, TimeTableService>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
