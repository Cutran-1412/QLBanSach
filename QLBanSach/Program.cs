using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QLBanSach.Data;
using QLBanSach.Data.NguoiDungRepository;
using QLBanSach.Data.SachRepository;
using QLBanSach.Data.TheLoaiRepository;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext") ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")));
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<ISacRepository,SachRepository>();
builder.Services.AddScoped<ITheLoaiRepository, TheLoaiRepository>();
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
