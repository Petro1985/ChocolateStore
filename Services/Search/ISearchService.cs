using Services.Models;

namespace Services.Search;

public interface ISearchService
{
    Task<SearchResult> GlobalSearch(string searchString);
}