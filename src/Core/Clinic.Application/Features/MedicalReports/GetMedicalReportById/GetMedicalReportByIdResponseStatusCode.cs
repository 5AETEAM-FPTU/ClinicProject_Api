namespace Clinic.Application.Features.MedicalReports.GetMedicalReportById;

public enum GetMedicalReportByIdResponseStatusCode
{
    OPERATION_SUCCESS,
    DATABASE_OPERATION_FAIL,
    MEDICAL_REPORT_NOT_FOUND,
    UNAUTHORIZE,
    FORBIDEN_ACCESS
    
}