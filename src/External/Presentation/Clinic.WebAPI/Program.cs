using System;
using System.Text;
using System.Threading;
using Clinic.AppBackgroundJob;
using Clinic.Application;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Commons.Entities;
using Clinic.Firebase;
using Clinic.JsonWebToken;
using Clinic.MailKit;
using Clinic.MySQL;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Data.DataSeeding;
using Clinic.OTP;
using Clinic.Redis;
using Clinic.WebAPI;
using Clinic.WebAPI.Commons.Middleware;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.JsonWebTokens;

// Default setting.
Console.OutputEncoding = Encoding.UTF8;
JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var config = builder.Configuration;

services.ConfigWebApi(configuration: config);
services.ConfigApplication();
services.ConfigMySqlRelationalDatabase(configuration: config);
services.ConfigGoogleSmtpMailNotification(configuration: config);
services.ConfigAppOTP();
services.ConfigFirebaseImageStorage();
services.ConfigureJwtIdentityService();
services.ConfigRedisCachingDatabase(configuration: config);
services.ConfigAppBackgroundJob();

var app = builder.Build();

// Data seeding.
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.Resolve<ClinicContext>();

    // Can database be connected.
    var canConnect = await context.Database.CanConnectAsync();

    // Database cannot be connected.
    if (!canConnect)
    {
        throw new HostAbortedException(message: "Cannot connect database.");
    }

    // Try seed data.
    var seedResult = await ClinicDataSeeding.SeedAsync(
        context: context,
        userManager: scope.Resolve<UserManager<User>>(),
        roleManager: scope.Resolve<RoleManager<Role>>(),
        defaultUserAvatarAsUrlHandler: scope.Resolve<IDefaultUserAvatarAsUrlHandler>(),
        cancellationToken: CancellationToken.None
    );

    // Data cannot be seed.
    if (!seedResult)
    {
        throw new HostAbortedException(message: "Database seeding is false.");
    }
}

// Configure the HTTP request pipeline.
app.UseMiddleware<GlobalExceptionHandler>()
    .UseCors()
    .UseAuthentication()
    .UseAuthorization()
    .UseResponseCaching()
    .UseFastEndpoints()
    .UseRateLimiter()
    .UseSwaggerGen()
    .UseSwaggerUi(configure: options =>
    {
        options.Path = string.Empty;
        options.DefaultModelsExpandDepth = -1;
    });

app.Run();
