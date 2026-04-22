using Asp.Versioning.ApiExplorer;
using Observex.Application.DependencyInjection;
using Observex.API.Configurations;
using Observex.Infrastructure.Utils;
using Observex.API.Hubs;
using Observex.Core.Identity;
using Observex.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Observex.Application.Interfaces;
using Observex.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services
    .AddApiVersioningServices()
    .AddSwaggerServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices()
    .AddSignalRService()
    .AddNotificationHubService()
    .AddIdentity<ApplicationUser, ApplicationRole>(op =>
    {
        op.Password.RequiredLength = 6;
        op.Password.RequireNonAlphanumeric = false;
        op.Password.RequireUppercase = false;
        op.Password.RequireLowercase = true;
        op.Password.RequireDigit = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddUserStore<UserStore<ApplicationUser, ApplicationRole, AppDbContext, Guid>>()
    .AddRoleStore<RoleStore<ApplicationRole, AppDbContext, Guid>>();

builder.Services.AddTransient<IJwtService, JwtService>();

//builder.Services.AddApplicationServices();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                $"swagger/{description.GroupName}/swagger.json",
                $"Observex API {description.GroupName.ToUpperInvariant()}");
        };
        options.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
    });
}

app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

// Mapping NotificationHub
app.MapHub<NotificationHub>("/notificationHub");
app.Run();
