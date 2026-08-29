using Maincipito.Domain.Entities;
using Maincipito.Domain.Repositories;
using Maincipito.Persistence;
using Maincipito.Persistence.Repositories;
using Maincipito.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. Configuración de Servicios (Inyección de Dependencias)
// ==========================================

// Interfaz Web y Vistas
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// Base de Datos de Dominio (SQL Server en Docker)
builder.Services.AddDbContext<MaincipitoDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MyAppContext")
        ?? throw new InvalidOperationException("Cadena de conexión 'MyAppContext' no encontrada."),
        sqlOptions => sqlOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

// Base de Datos de Identidad y Seguridad (SQL Server en Docker)
builder.Services.AddDbContext<IdentityDataContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("IdentityDataContextConnection")
        ?? throw new InvalidOperationException("Cadena de conexión 'IdentityDataContextConnection' no encontrada.")));

// ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<IdentityDataContext>();

// Repositorios de Dominio (Ciclo de Vida Scoped)
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<INurseRepository, NurseRepository>();
builder.Services.AddScoped<IRelativeRepository, RelativeRepository>();
builder.Services.AddScoped<IHistoryRepository, HistoryRepository>();
builder.Services.AddScoped<IVitalSignRepository, VitalSignRepository>();
builder.Services.AddScoped<IRepository<CareSuggestion>, Repository<CareSuggestion>>();

// ==========================================
// 2. Construcción de la Aplicación y Pipeline HTTP
// ==========================================
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

// ==========================================
// 3. Inicialización de Datos Automática (Seeding)
// ==========================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<MaincipitoDbContext>();
        await DbInitializer.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error al inicializar la base de datos con datos de prueba.");
    }
}

app.Run();