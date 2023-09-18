using Microsoft.EntityFrameworkCore;
using Reminder.Controllers;
using Reminder.Data;
using Reminder.Data.Repositories;
using Reminder.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ReminderDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbContext")));
builder.Services.AddScoped<EventoService>();
builder.Services.AddScoped<EventoRepository>();
builder.Services.AddScoped<UsuarioRepository>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
