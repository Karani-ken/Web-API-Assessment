using Microsoft.EntityFrameworkCore;
using Web_API_Assessment.Data;
using Web_API_Assessment.Extensions;
using Web_API_Assessment.Services;
using Web_API_Assessment.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//database connection

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});
//inject services
builder.Services.AddScoped<IUserInterface, UserServices>();
builder.Services.AddScoped<IEventInterface, EventService>();

//auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//add authenitication
builder.AddAppAuthentication();

//add authorization
builder.addAdminAuthorization();

//swagger api configurations
builder.AddSwaggenGenExtension();

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
app.ApplyMigration();
app.Run();
