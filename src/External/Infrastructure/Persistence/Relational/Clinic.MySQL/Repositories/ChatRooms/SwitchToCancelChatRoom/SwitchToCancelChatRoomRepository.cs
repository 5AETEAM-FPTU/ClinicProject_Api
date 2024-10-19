﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Appointments.SwitchToCancelChatRoom;
using Clinic.MySQL.Data.Context;
using Clinic.MySQL.Data.DataSeeding;
using Microsoft.EntityFrameworkCore;

namespace Clinic.MySQL.Repositories.ChatRooms.SwitchToCancelChatRoom;

/// <summary>
///     Implementation of ISwitchToCancelChatRoomRepository.
/// </summary>
internal class SwitchToCancelChatRoomRepository : ISwitchToCancelChatRoomRepository
{
    private readonly ClinicContext _context;
    private DbSet<Appointment> _appointments;

    public SwitchToCancelChatRoomRepository(ClinicContext context)
    {
        _context = context;
        _appointments = _context.Set<Appointment>();
    }

    public async Task<bool> SwitchToCancelChatRoom(CancellationToken cancellationToken = default)
    {
        var dbTransactionResult = false;

        await _context
            .Database.CreateExecutionStrategy()
            .ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(
                    cancellationToken
                );
                try
                {
                    await _appointments
                        .Where(predicate: entity =>
                            entity.StatusId == EnumConstant.AppointmentStatus.PENDING
                            && entity.ExaminationDate < DateTime.Now
                        )
                        .ExecuteUpdateAsync(builder =>
                            builder.SetProperty(
                                entity => entity.StatusId,
                                EnumConstant.AppointmentStatus.NO_SHOW
                            )
                        );
                    await transaction.CommitAsync(cancellationToken: cancellationToken);
                }
                catch
                {
                    await transaction.RollbackAsync(cancellationToken: cancellationToken);
                }
            });

        return dbTransactionResult;
    }
}