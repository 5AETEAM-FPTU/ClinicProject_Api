using Clinic.Application.Features.Admin.GetAllMedicineGroup;

namespace Clinic.WebAPI.EndPoints.Admin.GetAllMedicineGroup.Common;

/// <summary>
///     Represents the GetAllMedicineGroup state bag.
/// </summary>
internal sealed class GetAllMedicineGroupStateBag
{
    internal static string CacheKey { get; set; }

    internal static int CacheDurationInSeconds { get; } = 60;
    internal GetAllMedicineGroupRequest AppRequest { get; } = new();
}
