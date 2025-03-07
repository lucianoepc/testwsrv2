using Microsoft.EntityFrameworkCore;
using PoC.TestWServ2.Common.Ports;
using PoC.TestWSrv2.BusinessLogic;
using PoC.TestWSrv2.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure PostgreSQL
var connectionString = Environment.GetEnvironmentVariable("DB_URL") ?? 
    "Host=127.0.0.1;Port=5432;Database=dbfenix;Username=usr_owner_fnx;Password=mysecret";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register repositories
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IIdentityDocumentTypeRepository, IdentityDocumentTypeRepository>();
builder.Services.AddScoped<ISoldProductsRepository, SoldProductsRepository>();

// Register services
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIdentityDocumentTypeService, IdentityDocumentTypeService>();
builder.Services.AddScoped<ISoldProductsService, SoldProductsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
}

app.Run();
