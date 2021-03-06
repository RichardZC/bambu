using Hra.App.Models;
using Hra.App.Servicio;
using Hra.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

var Constantes = new Constantes();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.GetSection("Constante").Bind(Constantes);
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication("Hra").AddCookie("Hra", config =>
{
    config.Cookie.Name = "Hra";
    config.LoginPath = "/Seguridad";
    config.AccessDeniedPath = "/Home";
    config.ExpireTimeSpan = TimeSpan.FromHours(8);
});

builder.Services.AddSingleton<IConstante>(Constantes);
builder.Services.AddDbContext<BAMBUContext>(db => db.UseSqlServer(builder.Configuration.GetConnectionString("connectionDB")));
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosLocal>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
