using System;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Data.DataSeeding;

public static class ClinicDataSeeding
{
    public static async Task<bool> SeedAsync(
        ClinicContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler,
        CancellationToken cancellationToken
    )
    {
        try
        {
            // Seed lookup tables independently (each checks if already seeded)
            await SeedGendersAsync(context, cancellationToken);
            await SeedPositionsAsync(context, cancellationToken);
            await SeedSpecialtiesAsync(context, cancellationToken);
            await SeedRetreatmentTypesAsync(context, cancellationToken);
            await SeedAppointmentStatusesAsync(context, cancellationToken);
            await SeedRolesAsync(context, roleManager, cancellationToken);
            await SeedUsersAsync(context, userManager, cancellationToken);

            Console.WriteLine("[Seeding] All seed data applied successfully.");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Seeding] FATAL: {ex.Message}");
            Console.WriteLine($"[Seeding] Inner: {ex.InnerException?.Message}");
            return false;
        }
    }

    private static async Task SeedGendersAsync(
        ClinicContext context,
        CancellationToken ct
    )
    {
        var genders = context.Set<Gender>();
        if (await genders.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] Genders already seeded, skipping.");
            return;
        }

        await genders.AddRangeAsync(GenderSeeding.InitGenders(), ct);
        await context.SaveChangesAsync(ct);
        Console.WriteLine("[Seeding] Genders seeded.");
    }

    private static async Task SeedPositionsAsync(
        ClinicContext context,
        CancellationToken ct
    )
    {
        var positions = context.Set<Position>();
        if (await positions.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] Positions already seeded, skipping.");
            return;
        }

        await positions.AddRangeAsync(PositionSeeding.InitPositions(), ct);
        await context.SaveChangesAsync(ct);
        Console.WriteLine("[Seeding] Positions seeded.");
    }

    private static async Task SeedSpecialtiesAsync(
        ClinicContext context,
        CancellationToken ct
    )
    {
        var specialties = context.Set<Specialty>();
        if (await specialties.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] Specialties already seeded, skipping.");
            return;
        }

        await specialties.AddRangeAsync(SpecialtySeeding.InitSpecialties(), ct);
        await context.SaveChangesAsync(ct);
        Console.WriteLine("[Seeding] Specialties seeded.");
    }

    private static async Task SeedRetreatmentTypesAsync(
        ClinicContext context,
        CancellationToken ct
    )
    {
        var types = context.Set<RetreatmentType>();
        if (await types.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] RetreatmentTypes already seeded, skipping.");
            return;
        }

        await types.AddRangeAsync(RetreatmentTypeSeeding.InitRetreatmentTypes(), ct);
        await context.SaveChangesAsync(ct);
        Console.WriteLine("[Seeding] RetreatmentTypes seeded.");
    }

    private static async Task SeedAppointmentStatusesAsync(
        ClinicContext context,
        CancellationToken ct
    )
    {
        var statuses = context.Set<AppointmentStatus>();
        if (await statuses.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] AppointmentStatuses already seeded, skipping.");
            return;
        }

        await statuses.AddRangeAsync(AppointmentStatusSeeding.InitAppointmentStatuses(), ct);
        await context.SaveChangesAsync(ct);
        Console.WriteLine("[Seeding] AppointmentStatuses seeded.");
    }

    private static async Task SeedRolesAsync(
        ClinicContext context,
        RoleManager<Role> roleManager,
        CancellationToken ct
    )
    {
        var roles = context.Set<Role>();
        if (await roles.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] Roles already seeded, skipping.");
            return;
        }

        foreach (var newRole in CommonSeeding.InitNewRoles())
        {
            var result = await roleManager.CreateAsync(role: newRole);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"[Seeding] Role error: {error.Code} - {error.Description}");
                }
            }
        }
        Console.WriteLine("[Seeding] Roles seeded.");
    }

    private static async Task SeedUsersAsync(
        ClinicContext context,
        UserManager<User> userManager,
        CancellationToken ct
    )
    {
        var users = context.Set<User>();
        if (await users.AnyAsync(ct))
        {
            Console.WriteLine("[Seeding] Users already seeded, skipping.");
            return;
        }

        // Seed user
        var user = CommonSeeding.InitUser();
        await CreateUserWithRoleAsync(userManager, user, "Admin123@", "user");

        // Seed admin
        var admin = CommonSeeding.InitAdmin();
        await CreateUserWithRoleAsync(userManager, admin, "Admin123@", "admin");

        // Seed staff
        var staff = CommonSeeding.InitStaff();
        await CreateUserWithRoleAsync(userManager, staff, "Admin123@", "staff");

        // Seed doctor
        var doctor = CommonSeeding.InitDoctor();
        await CreateUserWithRoleAsync(userManager, doctor, "Admin123@", "doctor");

        Console.WriteLine("[Seeding] Users seeded.");
    }

    private static async Task CreateUserWithRoleAsync(
        UserManager<User> userManager,
        User user,
        string password,
        string role
    )
    {
        var createResult = await userManager.CreateAsync(user: user, password: password);
        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
            {
                Console.WriteLine($"[Seeding] CreateUser '{user.UserName}' error: {error.Code} - {error.Description}");
            }
            return;
        }

        var roleResult = await userManager.AddToRoleAsync(user: user, role: role);
        if (!roleResult.Succeeded)
        {
            foreach (var error in roleResult.Errors)
            {
                Console.WriteLine($"[Seeding] AddToRole '{user.UserName}' -> '{role}' error: {error.Code} - {error.Description}");
            }
            return;
        }

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user: user);
        await userManager.ConfirmEmailAsync(user: user, token: token);

        Console.WriteLine($"[Seeding] User '{user.UserName}' created with role '{role}'.");
    }
}
