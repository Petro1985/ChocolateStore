namespace Services.Models;

public class PagedItems<T>
{
    public int TotalCount { get; set; }
    public IReadOnlyCollection<T> Items { get; set; }

    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}