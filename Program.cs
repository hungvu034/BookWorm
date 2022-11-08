using System;
using BookWorn.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookWorn.Service ;
using Microsoft.AspNetCore.Identity.UI.Services;
using BookWorm.Service;
using BookWorm.Repository ;
using BookWorm.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
string ConnectionString = builder.Configuration.GetSection("ConnectionString").Value;
// Add services to the container.
builder.Services.Configure<MailSender.MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IEmailSender , MailSender>(); 
builder.Services.AddDbContext<EntityModel>(
    builder => builder.UseSqlServer(ConnectionString)
);

builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = true ; 
    options.Password.RequireDigit = false ; 
    options.Password.RequiredUniqueChars = 0; 
    options.Password.RequireUppercase = false ; 
    options.Password.RequireNonAlphanumeric = false  ;
    options.Password.RequireLowercase = false ; 
    options.Password.RequiredLength = 6 ;
    options.User.RequireUniqueEmail = true; 
    }
)
    .AddEntityFrameworkStores<EntityModel>()
    .AddDefaultTokenProviders()
    ;
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<ProductService>(); 
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<BillRepository>();
builder.Services.AddScoped<BuyService>();
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.ConfigureApplicationCookie(options=> {
    options.LoginPath = "/Sign In" ; 
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Identity/Account/AccessDeined" ; 
});
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    
    app.UseHsts();
}
app.MigrateDb();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}");

app.Run();

