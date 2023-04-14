using Microsoft.AspNetCore.Builder;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using MySite;
using MySite.BL.Implementations;
using MySite.BL.Interfaces;
using MySite.DAL.Implementations;
using MySite.DAL.Interfaces;
using MySite.UI.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ISongDAL,SongDAL>();
builder.Services.AddSingleton<ISongBL,SongBL>();

var app = builder.Build();



if(!app.Environment.IsDevelopment())
{   
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "admin",
        pattern: "/admin",
        defaults: new { controller = "Admin",action = "Choose" });
});


app.Run();

