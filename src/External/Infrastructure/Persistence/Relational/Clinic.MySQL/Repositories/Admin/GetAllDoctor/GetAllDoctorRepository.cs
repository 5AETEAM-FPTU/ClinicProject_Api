using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Clinic.Domain.Commons.Entities;
using Clinic.Domain.Features.Repositories.Admin.GetAllDoctor;
using Clinic.MySQL.Data.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Clinic.MySQL.Repositories.Admin.GetAllDoctor;

internal class GetAllDoctorRepository : IGetAllDoctorsRepository
{
    private readonly ClinicContext _context;
    private DbSet<User> _users;

    public GetAllDoctorRepository(ClinicContext context)
    {
        _context = context;
        _users = _context.Set<User>();
    }

    public Task<int> CountAllDoctorsQueryAsync(CancellationToken cancellationToken)
    {
        return _users
            .AsNoTracking()
            .Where(predicate: user =>
                user.Doctor != null
                && user.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
                && user.RemovedBy
                    == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
                && user.UserRoles.Any(userRole => userRole.Role.Name == "doctor")
            )
            .CountAsync(cancellationToken: cancellationToken);
    }

    public async Task<IEnumerable<User>> FindAllDoctorsQueryAsync(
        int pageIndex,
        int pageSize,
        CancellationToken cancellationToken
    )
    {
        //return await _users
        //    .AsNoTracking()
        //    .Where(predicate: user =>
        //        user.Doctor != null
        //        && user.RemovedAt == Application.Commons.Constance.CommonConstant.MIN_DATE_TIME
        //        && user.RemovedBy
        //            == Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
        //        && user.UserRoles.Select(userRole => userRole.Role.Name == "doctor").Any()
        //    )
        //    .Select(selector: user => new User()
        //    {
        //        Id = user.Id,
        //        UserName = user.UserName,
        //        FullName = user.FullName,
        //        PhoneNumber = user.PhoneNumber,
        //        Avatar = user.Avatar,
        //        Gender = new()
        //        {
        //            Id = user.Gender.Id,
        //            Name = user.Gender.Name,
        //            Constant = user.Gender.Constant,
        //        },
        //        Doctor = new()
        //        {
        //            DOB = user.Doctor.DOB,
        //            Description = user.Doctor.Description,
        //            Position = new()
        //            {
        //                Id = user.Doctor.Position.Id,
        //                Name = user.Doctor.Position.Name,
        //                Constant = user.Doctor.Position.Constant,
        //            },
        //            DoctorSpecialties = user
        //                .Doctor.DoctorSpecialties.Select(doctorSpecialty => new DoctorSpecialty()
        //                {
        //                    Specialty = new Specialty
        //                    {
        //                        Id = doctorSpecialty.Specialty.Id,
        //                        Name = doctorSpecialty.Specialty.Name,
        //                        Constant = doctorSpecialty.Specialty.Constant,
        //                    },
        //                })
        //                .ToList(),
        //            Address = user.Doctor.Address,
        //            Achievement = user.Doctor.Achievement,
        //            IsOnDuty = user.Doctor.IsOnDuty,
        //        },
        //    })
        //    .Skip((pageIndex - 1) * pageSize)
        //    .Take(pageSize)
        //    .ToListAsync(cancellationToken: cancellationToken);



        var connection = _context.Database.GetDbConnection();

        string sqlQuery =
            @"
        SELECT `t`.`Id`, `t`.`UserName`, `t`.`FullName`, `t`.`PhoneNumber`, `t`.`Avatar`, `g`.`Id`, `g`.`Name`, `g`.`Constant`, `t`.`DOB`, `t`.`Description`, `p`.`Id`,
        `p`.`Name`, `p`.`Constant`, `t`.`UserId`, `t0`.`Id`, `t0`.`Name`, `t0`.`Constant`, `t0`.`DoctorId`, `t0`.`SpecialtyID`, `t`.`Address`, `t`.`Achievement`, `t`.`IsOnDuty`
        FROM (
          SELECT `u`.`Id`, `u`.`Avatar`, `u`.`FullName`, `u`.`GenderId`, `u`.`PhoneNumber`, `u`.`UserName`, `d`.`UserId`, `d`.`Achievement`, `d`.`Address`, `d`.`DOB`,
            `d`.`Description`, `d`.`IsOnDuty`, `d`.`PositionId`
          FROM `Users` AS `u`
          LEFT JOIN `Doctors` AS `d` ON `u`.`Id` = `d`.`UserId`
          WHERE ((`d`.`UserId` IS NOT NULL AND (`u`.`RemovedAt` = '0001-01-01 00:00:00.000000')) AND (`u`.`RemovedBy` = '00000000-0000-0000-0000-000000000000')) AND EXISTS (
              SELECT 1
              FROM `UserRoles` AS `u0`
              INNER JOIN `Roles` AS `r` ON `u0`.`RoleId` = `r`.`Id`
              WHERE ((`u`.`Id` = `u0`.`UserId`)) AND (`r`.`Name` = 'doctor'))
          LIMIT 10 OFFSET 0
      ) AS `t`
      INNER JOIN `Genders` AS `g` ON `t`.`GenderId` = `g`.`Id`
      LEFT JOIN `Positions` AS `p` ON `t`.`PositionId` = `p`.`Id`
      LEFT JOIN (
          SELECT `s`.`Id`, `s`.`Name`, `s`.`Constant`, `d0`.`DoctorId`, `d0`.`SpecialtyID`
          FROM `DoctorSpecialties` AS `d0`
          INNER JOIN `Specialtys` AS `s` ON `d0`.`SpecialtyID` = `s`.`Id`
      ) AS `t0` ON `t`.`UserId` = `t0`.`DoctorId`
      ORDER BY `t`.`Id`, `t`.`UserId`, `g`.`Id`, `p`.`Id`, `t0`.`DoctorId`, `t0`.`SpecialtyID`";

        string sqlQuery2 =
            @"
            SELECT
            US.Id AS UserId,
            US.Username AS UserName,
            US.Email AS UserEmail,
            US.GenderId AS UserGenderId,
    
            DO.UserId AS DoctorId,
            DO.UserId AS DoctorUserId,
            DO.PositionId AS DoctorPositionId,
    
            PS.Id AS PositionId,
            PS.Name AS PositionName,
    
            DS.DoctorId AS DS_DoctorId,
            DS.SpecialtyId AS DS_SpecialtyId,
    
            SP.Id AS SpecialtyId,
            SP.Name AS SpecialtyName,
    
            GE.Id AS GenderId,
            GE.Name AS GenderName
            FROM Users AS US
            INNER JOIN Doctors AS DO ON DO.UserId = US.Id
            INNER JOIN UserRoles AS UR ON UR.UserId = DO.UserId
            INNER JOIN Roles AS RO ON RO.Id = UR.RoleId AND RO.Name = 'doctor'
            INNER JOIN Positions AS PS ON PS.Id = DO.PositionId
            INNER JOIN DoctorSpecialties AS DS ON DS.DoctorId = DO.UserId
            INNER JOIN Specialtys AS SP ON SP.Id = DS.SpecialtyId
            INNER JOIN Genders AS GE ON GE.Id = US.GenderId
            WHERE US.RemovedAt = @MinDateTime AND US.RemovedBy = @DefaultEntityId
            LIMIT @pageSize OFFSET @offset";

        // Tham số để truyền cho câu truy vấn SQL
        var parameters = new
        {
            pageSize = pageSize,
            offset = (pageIndex - 1) * pageSize,
            MinDateTime = Application.Commons.Constance.CommonConstant.MIN_DATE_TIME,
            DefaultEntityId = Application.Commons.Constance.CommonConstant.DEFAULT_ENTITY_ID_AS_GUID
        };

        // Dictionary để lưu trữ kết quả người dùng
        var userDictionary = new Dictionary<Guid, User>();

        // Ánh xạ dữ liệu
        var users = await connection.QueryAsync<
            User,
            Clinic.Domain.Commons.Entities.Doctor,
            Position,
            DoctorSpecialty,
            Specialty,
            Gender,
            User
        >(
            sql: sqlQuery2,
            map: (user, doctor, position, doctorSpecialty, specialty, gender) =>
            {
                if (!userDictionary.TryGetValue(user.Id, out var existingUser))
                {
                    existingUser = user;
                    existingUser.Doctor = doctor;
                    existingUser.Doctor.Position = position;
                    existingUser.Doctor.DoctorSpecialties = new List<DoctorSpecialty>();
                    existingUser.Gender = gender;

                    userDictionary[user.Id] = existingUser;
                }

                if (
                    specialty != null
                    && !existingUser.Doctor.DoctorSpecialties.Any(ds =>
                        ds.Specialty.Id == specialty.Id
                    )
                )
                {
                    existingUser
                        .Doctor.DoctorSpecialties.ToList()
                        .Add(new DoctorSpecialty { Specialty = specialty });
                }

                return existingUser;
            },
            param: parameters,
            splitOn: "DoctorId,PositionId,DS_DoctorId,SpecialtyId,GenderId"
        );

        return userDictionary.Values;
    }
}
