namespace Stockify.Common.Application.Pagination;

public record PagedResponse<T>(int Page, int PageSize, int TotalCount, IReadOnlyCollection<T> Data);
