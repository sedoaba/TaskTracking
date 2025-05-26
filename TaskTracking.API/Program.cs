using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TaskTracking.Application.Auth;
using TaskTracking.Infrastructure;
using TaskTracking.Infrastructure.Auth;
using TaskTracking.Infrastructure.Seeding;
using Hangfire;
using Hangfire.SqlServer;
using TaskTracking.Infrastructure.Services;
using TaskTracking.Infrastructure.Scheduling;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Secret"]))
        };
    });

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddSingleton(builder.Configuration.GetSection("Jobs"));

builder.Services.AddHangfire(config =>
        config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
          .UseSimpleAssemblyNameTypeSerializer()
          .UseRecommendedSerializerSettings()
          .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
          {
              CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
              SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
              QueuePollInterval = TimeSpan.FromSeconds(15),
              UseRecommendedIsolationLevel = true,
              DisableGlobalLocks = true
          }));

builder.Services.AddHangfireServer();


// Register application services
builder.Services.AddScoped<ITaskService, TaskService>();

builder.Services.AddScoped<ITaskStatusScheduler, TaskStatusScheduler>();




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseHangfireDashboard("/hangfire");
}


using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
    DbInitializer.Seed(dbContext);
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var configuration = app.Services.GetRequiredService<IConfiguration>();
var overdueCron = configuration["Jobs:OverdueTaskCron"] ?? "0 * * * *";

// Start Hangfire recurring jobs
RecurringJob.AddOrUpdate<ITaskStatusScheduler>(
    "OverdueTask",
    scheduler => scheduler.UpdateOverduTaskAsync(),
    overdueCron);

app.Run();
