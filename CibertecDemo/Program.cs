using CibertecDemo.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Registrar el repositorio ADO.NET
builder.Services.AddScoped<ProductoRepository>();

//Agregar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyHeader().
               AllowAnyMethod().
               AllowAnyOrigin();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>     //Configuracion de swagger
{                                       //Encargado de documentar automaticamente el proyecto
    c.SwaggerDoc("v1", new OpenApiInfo  //para desarrolladores
    {
        Title = "DSW I Cibertec Demo API",
        Version = "v1"
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DSWI Cibertec Demo API v1");
    });
}
else {
    app.UseExceptionHandler();
    app.UseHsts();
}

//Ultima accion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
