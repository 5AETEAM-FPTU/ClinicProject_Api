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
    /// <summary>
    ///     Seed data.
    /// </summary>
    /// <param name="context">
    ///     Database context for interacting with other table.
    /// </param>
    /// <param name="userManager">
    ///     Specific manager for interacting with user table.
    /// </param>
    /// <param name="roleManager">
    ///     Specific manager for interacting with role table.
    /// </param>
    /// <param name="cancellationToken">
    ///     A token that is used for notifying system
    ///     to cancel the current operation when user stop
    ///     the request.
    /// </param>
    /// <returns>
    ///     True if seeding is success. Otherwise, false.
    /// </returns>
    public static async Task<bool> SeedAsync(
        ClinicContext context,
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IDefaultUserAvatarAsUrlHandler defaultUserAvatarAsUrlHandler,
        CancellationToken cancellationToken
    )
    {
        var executedTransactionResult = false;
        var roles = context.Set<Role>();
        var genders = context.Set<Gender>();
        var positions = context.Set<Position>();
        var retreatmentTypes = context.Set<RetreatmentType>();
        var specialties = context.Set<Specialty>();
        var statusAppointment = context.Set<AppointmentStatus>();
        // continue....

        var isTableEmpty = await IsTableEmptyAsync(
            roles: roles,
            cancellationToken: cancellationToken
        );

        if (!isTableEmpty)
        {
            return true;
        }

        // Init list of datas.
        var newRoles = CommonSeeding.InitNewRoles();
        var admin = CommonSeeding.InitAdmin();
        var staff = CommonSeeding.InitStaff();
        var doctor = CommonSeeding.InitDoctor();
        var user = CommonSeeding.InitUser();

        var newGenders = GenderSeeding.InitGenders();
        var newSpecialties = SpecialtySeeding.InitSpecialties();
        var newPositions = PositionSeeding.InitPositions();
        var newRetreatTypes = RetreatmentTypeSeeding.InitRetreatmentTypes();
        var newStatusAppointments = AppointmentStatusSeeding.InitAppointmentStatuses();

        await context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(operation: async () =>
            {
                await using var dbTransaction = await context.Database.BeginTransactionAsync(
                    cancellationToken: cancellationToken
                );

                try
                {
                    // Init roles.
                    foreach (var newRole in newRoles)
                    {
                        await roleManager.CreateAsync(role: newRole);
                    }

                    // Init genders.
                    await genders.AddRangeAsync(newGenders, cancellationToken);

                    // Init specialties.
                    await specialties.AddRangeAsync(newSpecialties, cancellationToken);

                    // Init positions.
                    await positions.AddRangeAsync(newPositions, cancellationToken);

                    // Init retreatment types.
                    await retreatmentTypes.AddRangeAsync(newRetreatTypes, cancellationToken);

                    // Init appointment statuses.
                    await statusAppointment.AddRangeAsync(newStatusAppointments, cancellationToken);

                    // Init user.
                    await userManager.CreateAsync(user: user, password: "Admin123@");
                    await userManager.AddToRoleAsync(user: user, role: "user");
                    var emailConfirmationToken3 =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: user);
                    await userManager.ConfirmEmailAsync(user: user, token: emailConfirmationToken3);

                    // Init admin.
                    await userManager.CreateAsync(user: admin, password: "Admin123@");
                    await userManager.AddToRoleAsync(user: admin, role: "admin");
                    var emailConfirmationToken =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: admin);
                    await userManager.ConfirmEmailAsync(user: admin, token: emailConfirmationToken);

                    // Init staff.
                    await userManager.CreateAsync(user: staff, password: "Admin123@");
                    await userManager.AddToRoleAsync(user: staff, role: "staff");
                    var emailConfirmationToken2 =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: staff);
                    await userManager.ConfirmEmailAsync(
                        user: staff,
                        token: emailConfirmationToken2
                    );

                    // Init doctor.
                    await userManager.CreateAsync(user: doctor, password: "Admin123@");
                    await userManager.AddToRoleAsync(user: doctor, role: "doctor");
                    var emailConfirmationToken4 =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: doctor);
                    await userManager.ConfirmEmailAsync(
                        user: doctor,
                        token: emailConfirmationToken4
                    );

                    await context.SaveChangesAsync(cancellationToken: cancellationToken);

                    await dbTransaction.CommitAsync(cancellationToken: cancellationToken);

                    executedTransactionResult = true;
                }
                catch
                {
                    await dbTransaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return executedTransactionResult;
    }

    private static async Task<bool> IsTableEmptyAsync(
        DbSet<Role> roles,
        CancellationToken cancellationToken
    )
    {
        // Is roles table empty.
        var isTableNotEmpty = await roles.AnyAsync(cancellationToken: cancellationToken);

        if (isTableNotEmpty)
        {
            return false;
        }

        // continue...


        return true;
    }
}
