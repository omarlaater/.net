using Examen.ApplicationCore.Interfaces;
using Examen.ApplicationCore.Services;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Enregistrer le contexte de base de données
builder.Services.AddDbContext<ApplicationDbContext>();

// Enregistrer l'unité de travail
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Enregistrer les services
builder.Services.AddScoped<IInfirmierService, InfirmierService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IBilanService, BilanService>();
builder.Services.AddScoped<IAnalyseService, AnalyseService>();
builder.Services.AddScoped<IService<Examen.ApplicationCore.Domain.Laboratoire>, Service<Examen.ApplicationCore.Domain.Laboratoire>>();
builder.Services.AddScoped<ILaboratoireService, LaboratoireService>();

var app = builder.Build();

// Initialiser la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    DataInitializer.Initialize(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
