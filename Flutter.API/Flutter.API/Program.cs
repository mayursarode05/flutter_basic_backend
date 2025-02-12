using Application.Mapper;
using Application.Repository.IRepository;
using Application.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// **Enable CORS**
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:7162", "https://10.0.2.2:7162/api") // **Change this to your Flutter app URL**
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
// **Use CORS Middleware**
app.UseCors("AllowFlutterApp");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
