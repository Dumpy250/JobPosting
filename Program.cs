using System;
using System.Threading.Tasks;
using JobPosting.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// -------------------- Services --------------------

// MVC controllers & views
builder.Services.AddControllersWithViews();

// Read connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// DbContext registration
builder.Services.AddDbContext<JobPostingContext>(options =>
options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 37))));

// Custom database test service
builder.Services.AddScoped<DatabaseService>();

var app = builder.Build();

// -------------------- Database Test (Safe + Awaited) --------------------
await TestDatabaseConnectionAsync(app);

// -------------------- Middleware Pipeline --------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthorization();

// MVC default routing
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// -------------------- Helper Methods --------------------

static async Task TestDatabaseConnectionAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;

    try
    {
        var databaseService = services.GetRequiredService<DatabaseService>();
        await databaseService.TestDatabaseConnectionAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Database startup check failed: {ex.Message}");
    }
}
