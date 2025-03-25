using Services;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.Sources.Clear();

// Add appsettings.Development.json as the primary configuration file
builder.Configuration
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddAuthorization();
builder.Services.AddHttpClient<IAuthApiServices, AuthApiServices>();
builder.Services.AddHttpClient<IDeviceApiServices, DeviceApiServices>();
builder.Services.AddHttpClient<IEmployeeApiServices, EmployeeApiServices>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();