var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "myInfo",
    pattern: "{controller=Home}/{action=MyInfo}"); 


app.MapDefaultControllerRoute();

app.Run();