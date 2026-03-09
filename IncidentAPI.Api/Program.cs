using Microsoft.EntityFrameworkCore;
using Serilog;
using IncidentAPI.Api.Data;
using IncidentAPI.Api.Middleware;
using IncidentAPI.Api.Repositories.Interfaces;
using IncidentAPI.Api.Repositories.Implementations;
using IncidentAPI.Api.Services.Interfaces;
using IncidentAPI.Api.Services.Implementations;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/incidentapi.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "IncidentAPI",
        Version = "v1",
        Description = "API Restful para incidentes técnicos"
    });
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=incidents.db"));


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IIncidentService, IncidentService>();
builder.Services.AddScoped<ICommentService, CommentService>();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}


app.Run();