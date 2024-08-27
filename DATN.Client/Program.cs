using Microsoft.AspNetCore.Identity;
using DATN.Client.Services;
using DATN.Core.AutoMapper;
using DATN.Core.Data;
using DATN.Core.Infrastructures;
using DATN.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Builder;
using System.Configuration;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.Eventing.Reader;
using DATN.Client;
using DATN.Client.Services;
using DATN.Core.AutoMapper;
using DATN.Core.Infrastructures;
using DATN.Core.Models;
using DATN.Core.Repositories.IRepositories;
using DATN.Core.Repositories.Repositories;
using DATN.Client.Controllers.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDistributedMemoryCache(); // Use a distributed cache for session data
builder.Services.AddHttpClient<FilterViewComponent>();
builder.Services.AddLogging();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie configuration for authentication cookies
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;


    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddSession(options =>
{
    // Session-specific configuration
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.IdleTimeout = TimeSpan.FromMinutes(60); // Set the session timeout
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllersWithViews();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    // Set the MinimumSameSitePolicy property to SameSiteMode.Lax
    options.MinimumSameSitePolicy = SameSiteMode.Lax;

    options.Secure = CookieSecurePolicy.Always;
    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.Always;
});

var connectionString = builder.Configuration.GetConnectionString("DATNDbContextConnection") ?? throw new InvalidOperationException("Connection string 'DATNDbContextConnection' not found.");

builder.Services.AddDbContext<DATNDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Set the default authentication scheme
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Set the default sign-in scheme
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Set the default challenge scheme
})
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/Identity/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Error/Error403";
    })
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        googleOptions.CallbackPath = "/signin-google";

    })
    .AddFacebook(facebookOptions =>
    {
        // Đọc cấu hình
        IConfigurationSection facebookAuthNSection = builder.Configuration.GetSection("Authentication:Facebook");
        facebookOptions.AppId = facebookAuthNSection["AppId"];
        facebookOptions.AppSecret = facebookAuthNSection["AppSecret"];

        // Thiết lập đường dẫn Facebook chuyển hướng đến
        facebookOptions.CallbackPath = "/signin-facebook";
    });
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole<Guid>>()
                 .AddEntityFrameworkStores<DATNDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedAccount = false;

    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = false;

    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;  // 1 tk chỉ được 1 email

});
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSession();

builder.Services.AddScoped<UserManager<AppUser>>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();

//register auto mapper
var mapper = AutoMapperConfig.Initialize();
builder.Services.AddSingleton(mapper);
// config client service
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ClientService>();

ExcelPackage.LicenseContext = LicenseContext.Commercial;
builder.Services.AddScoped<ExcelService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseSession();
app.UseOAuthMiddleware();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); 
app.MapControllerRoute(
    name: "Admin",
    pattern: "{areas=Admin}/{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();
app.Run();
