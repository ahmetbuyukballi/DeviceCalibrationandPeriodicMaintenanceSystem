using Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Infrastucture.Persistence;
using Infrastucture.DataSeeder;
using Infrastucture;
using Domain;
using Domain.Repository;
using Infrastucture.Repository;
using Serilog;
using DeviceCalibrationAndPeriodicMaintenanceSystemm.ExpectionMiddleware;
using ApplicationCore.Abstraction;
using ApplicationCore.Concrete;
using ApplicationCore.MailService;
using DeviceCalibrationAndPeriodicMaintenanceSystemm;
using Infrastucture.Audit;
using Org.BouncyCastle.Asn1.X509.Qualified;
using ApplicationCore.BaseService;
using OfficeOpenXml;
using StackExchange.Redis;


var builder = WebApplication.CreateBuilder(args);

// Serilog yapýlandýrmasý (ilk adýmda)
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(); // burada eklenmeli

// Servisleri ekle
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
//scopelar
builder.Services.ScopeService(builder.Configuration);

builder.Services.AddSwaggerCollection(builder.Configuration);

builder.Services.AddHostedService<NotificationService>();


//builder.Services.AddScoped(typeof(INotificationService), typeof(NotificationService));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());// Derinliði sýnýrla
    });
builder.Services.Configure<RouteOptions>(options => {
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<MailService>();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var configuration = "localhost";
    return ConnectionMultiplexer.Connect(configuration);
});
/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin rolü ara",
    policy => policy.RequireRole("admin"));
});*/
builder.Services.AddAuthorization();



var app = builder.Build();

// Veritabaný seed iþlemi
using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

    await DataSeeder.Seed(dbcontext, userManager, roleManager);
}

// Middleware'ler
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseMiddleware<ExpectionMiddleware>(); // kendi middleware'in
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

try
{
    Log.Information("Starting web application");
    app.Run(); // sadece burada olacak
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
