﻿namespace Clinic.WebAPI.EndPoints.Users.GetAllDoctor.HttpResponseMapper;
/// <summary>
///     GetAllDoctors extension method
/// </summary>
internal static class GetAllDoctorHttpResponseMapper
{
    private static GetAllDoctorHttpResponseManager _GetAllDoctorHttpResponseManager;

    internal static GetAllDoctorHttpResponseManager Get()
    {
        return _GetAllDoctorHttpResponseManager ??= new();
    }
}

