using LibraryModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Merca_Darius_ExamenNew.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Register LibraryContext as a Service
builder.Services.AddDbContext<LibraryContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Add Use Authentication for Identity and an endpoint for Razor pages
builder.Services.AddDbContext<IdentityContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configure Lockout
builder.Services.Configure<IdentityOptions>(options => {
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
    options.Lockout.MaxFailedAccessAttempts = 1;
    options.Lockout.AllowedForNewUsers = true;
    //Configure password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});
//Authorization policy
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("Employees", policy =>
    {
        policy.RequireRole("Employee");
    });
});
builder.Services.AddAuthorization(opts => {
    opts.AddPolicy("Managers", policy => 
    {
        policy.RequireRole("Manager");
        
    });
});
builder.Services.ConfigureApplicationCookie(opts =>
{
    opts.AccessDeniedPath = "/Identity/Account/AccessDenied";

});

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<IdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();


app.Run();
