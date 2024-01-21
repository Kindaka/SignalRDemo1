using Microsoft.EntityFrameworkCore;
using SignalRDemo.Services;
using SignalRDemo.MiddlewareExtensions;
using SignalRDemo.SubscribeTableDependencies;
using SignalRDemo.DBContext;
using SignalRDemo.Hubs;
using SignalRDemo.Models;
using SignalRDemo.Repos;
using SignalRDemo.Services.Interfaces;
using SignalRDemo.Services;
using Hangfire;
using Hangfire.Console;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISenderService, SenderService>();
builder.Services.AddSingleton<IRecipientService, RecipientService>();
builder.Services.AddSingleton<IUserGroupService, UserGroupService>();
builder.Services.AddSingleton<INotificationSettingService, NotificationSettingService>();
builder.Services.AddSingleton<INotificationService, NotificationService>();

//DB Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NotificationDBContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Singleton
);

//DI
builder.Services.AddSingleton<UserRepo>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<NotificationHub>();
builder.Services.AddSingleton<SubscribeNotificationTableDependency>();

//Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseConsole()
    .UseSqlServerStorage(connectionString)); // Sử dụng SQL Server storage

builder.Services.AddHangfireServer();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy
            .AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

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

app.UseAuthorization();

app.UseSession();

app.MapHub<NotificationHub>("/notificationHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=SignIn}/{id?}");

app.UseHangfireDashboard();

app.MapHangfireDashboard("/hangfire");

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = "api-docs";
});

app.UseSqlTableDependency<SubscribeNotificationTableDependency>(connectionString);

app.Run();
