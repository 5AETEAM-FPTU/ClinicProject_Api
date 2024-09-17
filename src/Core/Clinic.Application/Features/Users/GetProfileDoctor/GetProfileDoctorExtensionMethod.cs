using Clinic.Application.Commons.Abstractions.GetProfileUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Application.Features.Users.GetProfileDoctor;

public static class GetProfileDoctorExtensionMethod
{
    public static string ToAppCode(this GetProfileDoctorResponseStatusCode statusCode)
    {
        return $"{nameof(GetProfileDoctor)}Feature: {statusCode}";
    }
}
