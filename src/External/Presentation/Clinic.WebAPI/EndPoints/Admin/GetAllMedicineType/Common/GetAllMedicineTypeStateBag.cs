using Clinic.Application.Features.Admin.GetAllMedicineType;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicineType.Common;

/// <summary>
///     Represents the GetAllMedicineType state bag.
/// </summary>
internal sealed class GetAllMedicineTypeStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllMedicineTypeRequest AppRequest { get; } = new();
}
