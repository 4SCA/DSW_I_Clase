using CibertecDemo.Data;
using CibertecDemo.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

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
builder.Services.AddSwaggerGen(config =>     //Configuracion de swagger
{                                       //Encargado de documentar automaticamente el proyecto
    config.SwaggerDoc("v1", new OpenApiInfo  //para desarrolladores
    {
        Title = "DSW I Cibertec Demo API",
        Version = "v1"
    });
});
builder.Services.AddControllers();

var app = builder.Build();

app.MapGrpcService<ProductoGrpcService>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "DSWI Cibertec Demo API v1");
    });
}
else {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

//Ultima accion

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
