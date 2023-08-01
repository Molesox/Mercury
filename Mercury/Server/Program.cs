using Mercury.Server.Data;
using Mercury.Server.Data.Context;
using Mercury.Shared.Models;
using Mercury.Shared.Models.AspNetUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllersWithViews().ConfigureApiBehaviorOptions((opts) =>
{
    //opts.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddDbContext<MercuryContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddTransient<RepositoryEF<AspNetRole, MercuryContext>>();
builder.Services.AddTransient<RepositoryEF<AspNetUser, MercuryContext>>();

builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

var isDev = app.Environment.IsDevelopment();

// Configure the HTTP request pipeline.
if (isDev)
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseHttpsRedirection();
}


app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
