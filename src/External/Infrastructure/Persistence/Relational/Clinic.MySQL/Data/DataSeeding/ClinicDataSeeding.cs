using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Application.Commons.Constance;
using Clinic.Application.Commons.FIleObjectStorage;
using Clinic.Domain.Commons.Entities;
using Clinic.MySQL.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Data.DataSeeding;

public static class ClinicDataSeeding
{
    private static readonly Guid AdminId = Guid.Parse(
        input: "1a6c3e77-4097-40e2-b447-f00d1f82cf78"
    );

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
        // continue....

        var isTableEmpty = await IsTableEmptyAsync(
            roles: roles,
            cancellationToken: cancellationToken
        );

        if (!isTableEmpty)
        {
            return true;
        }

        // Init list of role.
        var newRoles = InitNewRoles();

        var admin = InitAdmin();

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

                    // Init admin.
                    await userManager.CreateAsync(user: admin, password: "Admin123@");

                    await userManager.AddToRoleAsync(user: admin, role: "admin");

                    var emailConfirmationToken =
                        await userManager.GenerateEmailConfirmationTokenAsync(user: admin);

                    await userManager.ConfirmEmailAsync(user: admin, token: emailConfirmationToken);

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

    private static User InitAdmin()
    {
        User admin =
            new()
            {
                Id = AdminId,
                UserName = "admin",
                Email = "nvdatdz8b@gmail.com",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                RemovedAt = CommonConstant.MIN_DATE_TIME,
                RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                UpdatedAt = CommonConstant.MIN_DATE_TIME,
                UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
            };

        return admin;
    }

    private static List<Role> InitNewRoles()
    {
        Dictionary<Guid, string> newRoleNames = [];

        Guid doctorRole = Guid.Parse(input: "c39aa1ac-8ded-46be-870c-115b200b09fc");
        Guid adminRole = Guid.Parse(input: "c8500b45-b134-4b60-85b7-8e6af1187e0a");
        Guid staffRole = Guid.Parse(input: "c8500b41-b134-4b60-85b7-8e6af1187e0b");
        Guid patienRole = Guid.Parse(input: "c8500b46-b134-4b60-85b7-8e6af1187e0c");

        newRoleNames.Add(key: doctorRole, value: "doctor");
        newRoleNames.Add(key: staffRole, value: "staff");
        newRoleNames.Add(key: patienRole, value: "patient");
        newRoleNames.Add(key: adminRole, value: "admin");

        List<Role> newRoles = [];

        foreach (var newRoleName in newRoleNames)
        {
            Role newRole =
                new()
                {
                    Id = newRoleName.Key,
                    Name = newRoleName.Value,
                    RoleDetail = new()
                    {
                        RoleId = newRoleName.Key,
                        CreatedAt = DateTime.UtcNow,
                        CreatedBy = AdminId,
                        UpdatedAt = CommonConstant.MIN_DATE_TIME,
                        UpdatedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                        RemovedAt = CommonConstant.MIN_DATE_TIME,
                        RemovedBy = CommonConstant.DEFAULT_ENTITY_ID_AS_GUID,
                    }
                };

            newRoles.Add(item: newRole);
        }

        return newRoles;
    }
}
