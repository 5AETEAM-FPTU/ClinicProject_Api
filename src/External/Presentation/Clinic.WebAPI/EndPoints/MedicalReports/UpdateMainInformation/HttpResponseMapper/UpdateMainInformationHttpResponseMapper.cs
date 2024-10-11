using Clinic.WebAPI.EndPoints.MedicalReports.CreateMedicalReport.HttpResponseMapper;

namespace Clinic.WebAPI.EndPoints.MedicalReports.UpdateMainInformation.HttpResponseMapper;

public class UpdateMainInformationHttpResponseMapper
{
    private static UpdateMainInformationHttpResponseManager _manager = new();

    internal static UpdateMainInformationHttpResponseManager Get() => _manager ??= new();
}
