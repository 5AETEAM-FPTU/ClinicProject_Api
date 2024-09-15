using System;
using Clinic.Domain.Commons.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Data.Context;

/// <summary>
///     Implementation of database context.
/// </summary>
public class ClinicContext : IdentityDbContext<User, Role, Guid>
{
    public ClinicContext(DbContextOptions<ClinicContext> options)
        : base(options) { }

    /// <summary>
    ///     Configure tables and seed initial data here.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
    /// </param>

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder: builder);

        builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

        RemoveAspNetPrefixInIdentityTable(builder: builder);
    }

    /// <summary>
    ///     Remove "AspNet" prefix in identity table name.
    /// </summary>
    /// <param name="builder">
    ///     Model builder access the database.
    /// </param>
    private static void RemoveAspNetPrefixInIdentityTable(ModelBuilder builder)
    {
        const string AspNetPrefix = "AspNet";

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var tableName = entityType.GetTableName();

            if (tableName.StartsWith(value: AspNetPrefix))
            {
                entityType.SetTableName(name: tableName[6..]);
            }
        }
    }
}
