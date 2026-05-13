namespace Faradars.Application.DTOs.Filters;

public class FilterRequest<TSearch> where TSearch : new()
{
    public PaginationDto Pagination { get; set; } = new();
    public TSearch Search { get; set; } = new();
}