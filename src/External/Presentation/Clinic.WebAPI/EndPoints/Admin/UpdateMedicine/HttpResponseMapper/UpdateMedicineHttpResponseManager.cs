using System.Collections.Generic;
using System;
using Clinic.Application.Features.Admin.UpdateMedicine;
using Microsoft.AspNetCore.Http;
using Clinic.Application.Features.Doctors.UpdatePrivateDoctorInfo;
using Clinic.Application.Features.Doctors.GetProfileDoctor;

namespace Clinic.WebAPI.EndPoints.Admin.UpdateMedicine.HttpResponseMapper;

public class UpdateMedicineHttpResponseManager
{
    private readonly Dictionary<
        UpdateMedicineResponseStatusCode,
        Func<UpdateMedicineRequest, UpdateMedicineResponse, UpdateMedicineHttpResponse>
    > _dictionary;

    internal UpdateMedicineHttpResponseManager()
    {
        _dictionary = [];

        _dictionary.Add(
            key: UpdateMedicineResponseStatusCode.OPERATION_SUCCESS,
            value: (_, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status200OK,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

        _dictionary.Add(
           key: UpdateMedicineResponseStatusCode.MEDICINE_IS_NOT_FOUND,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status404NotFound,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );
        
        _dictionary.Add(
           key: UpdateMedicineResponseStatusCode.MEDICINE_GROUP_ID_IS_NOT_FOUND,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status404NotFound,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );
         
        _dictionary.Add(
           key: UpdateMedicineResponseStatusCode.MEDICINE_TYPE_ID_IS_NOT_FOUND,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status404NotFound,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );

        _dictionary.Add(
           key: UpdateMedicineResponseStatusCode.DATABASE_OPERATION_FAIL,
           value: (_, response) =>
               new()
               {
                   HttpCode = StatusCodes.Status417ExpectationFailed,
                   AppCode = response.StatusCode.ToAppCode()
               }
       );

        _dictionary.Add(
            key: UpdateMedicineResponseStatusCode.FORBIDEN_ACCESS,
            value: (request, response) =>
                new()
                {
                    HttpCode = StatusCodes.Status403Forbidden,
                    AppCode = response.StatusCode.ToAppCode(),
                }
        );

    }

    internal Func<
        UpdateMedicineRequest,
        UpdateMedicineResponse,
        UpdateMedicineHttpResponse
    > Resolve(UpdateMedicineResponseStatusCode statusCode)
    {
        return _dictionary[statusCode];
    }
}
