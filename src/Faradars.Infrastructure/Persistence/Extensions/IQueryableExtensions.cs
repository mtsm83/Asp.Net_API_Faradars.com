using Faradars.Application.DTOs.Filters;

namespace Faradars.Infrastructure.Persistence.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, PaginationDto paginationDto)
    {
        return query
            .Skip((paginationDto.PageNumber - 1) * paginationDto.PageSize)
            .Take(paginationDto.PageSize);
    }
}