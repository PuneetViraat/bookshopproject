using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ProjectECommerce.DataAccess.Data;
using ProjectECommerce.DataAccess.Repoistory;
using ProjectECommerce.DataAccess.Repoistory.IRepository;
using ProjectECommerce.Utility;
using Stripe;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("con");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>().
    AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages();
//  builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//   builder.Services.AddScoped<ICoverTypeRepository, CoverTypeRepository>();

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.Configure<StripeSettings>
    (builder.Configuration.GetSection("StripeSettings"));
builder.Services.Configure<EmailSettings>
    (builder.Configuration.GetSection("EmailSettings"));


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
    options.LogoutPath = $"/Identity/Account/Logout";

});

builder.Services.AddAuthentication().AddFacebook(option =>
{
    option.AppId = "2152178134978903";
    option.AppSecret = "3790113ed6229a6a3d3d2aa3cee37906";

});

builder.Services.AddAuthentication().AddGoogle(option =>
{
    option.ClientId = "839574072545-6qnqmvk0nuaahf8amvtdntl4h6tktpm5.apps.googleusercontent.com";
    option.ClientSecret = "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";

});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
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
StripeConfiguration.ApiKey = 
    builder.Configuration.GetSection("StripeSettings")["SecretKey"];
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
