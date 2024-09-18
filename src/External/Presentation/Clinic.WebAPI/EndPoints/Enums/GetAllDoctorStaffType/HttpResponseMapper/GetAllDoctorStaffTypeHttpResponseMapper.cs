
namespace Clinic.WebAPI.EndPoints.Enums.GetAllDoctorStaffType.HttpResponseMapper;

/// <summary>
///     GetAllDoctorStaffType extension method
/// </summary>
internal static class GetAllDoctorStaffTypeHttpResponseMapper
{
    private static GetAllDoctorStaffTypeHttpResponseManager _GetAllDoctorStaffTypesHttpResponseManager;

    internal static GetAllDoctorStaffTypeHttpResponseManager Get()
    {
        return _GetAllDoctorStaffTypesHttpResponseManager ??= new();
    }
}
