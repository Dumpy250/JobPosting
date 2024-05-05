using JobPosting.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add this line to register JobPostingContext
builder.Services.AddDbContext<JobPostingContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 37))));

// Register DatabaseService
builder.Services.AddScoped<DatabaseService>();

var app = builder.Build();

// Test database connection
TestDatabaseConnection(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

void TestDatabaseConnection(WebApplication app)
{
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;
        try
        {
            var databaseService = services.GetRequiredService<DatabaseService>();
            databaseService.TestDatabaseConnectionAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while testing the database connection: {ex.Message}");
        }
    }
}