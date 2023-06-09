using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
var builder = WebApplication.CreateBuilder(args);



builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        options.UseSqlServer(builder.Configuration.GetConnectionString("MacOSConnection"));
});



builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
.AddRoles<Role>()
.AddRoleManager<RoleManager<Role>>()
.AddUserManager<UserManager<User>>()
.AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient(typeof(ILogger), typeof(Logger<Program>));
builder.Services.AddControllersWithViews();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseMigrationsEndPoint();
}
else
{
  app.UseExceptionHandler("/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcorehsts.
  app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();