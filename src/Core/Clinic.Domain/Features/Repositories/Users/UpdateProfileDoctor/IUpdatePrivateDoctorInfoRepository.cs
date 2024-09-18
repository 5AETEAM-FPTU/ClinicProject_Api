﻿
using Clinic.Domain.Commons.Entities;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Clinic.Domain.Features.Repositories.Users.UpdateProfileDoctor;

public interface IUpdatePrivateDoctorInfoRepository
{
    Task<bool> UpdatePrivateDoctorInfoByIdCommandAsync(
        User user, CancellationToken cancellationToken
    );

    public Task<User> GetDoctorByIdAsync(Guid userId, CancellationToken cancellationToken);
    
}