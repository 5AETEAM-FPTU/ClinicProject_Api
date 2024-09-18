using System.Collections.Generic;

namespace Clinic.Application.Commons.Pagination;

/// <summary>
///     Represent the pagination response model.
/// </summary>
public class PaginationResponse<T>
{
    public IEnumerable<T> Contents { get; init; }

    public int PageIndex { get; init; } = 1;

    public int PageSize { get; init; } = 20;

    public int TotalPages { get; init; }

    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;
}
