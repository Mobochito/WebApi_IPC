using Microsoft.EntityFrameworkCore;
using CrudApi.Context;
using CrudApi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar el DbContext SQL Server en el contenedor de servicios
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSql"));
});


// Registrar el DbContext Mysql en el contenedor de servicios
builder.Services.AddDbContext<AppDbMySqlContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("CadenaMySql"), 
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("CadenaMySql"))
        );
});

// Le dices a la app: "Cuando alguien pida IApplicationDbContext, dale AppDbMySqlContext"
builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbMySqlContext>());

/*
 *Add-Migration InicialCreateMysql -Context AppDbMySqlContext
 * Update-Database -Context AppDbMySqlContext
 */


builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<EmpleadoService>();

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
