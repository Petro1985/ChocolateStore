using ApiContracts.Search;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Services.Models;
using Services.Search;

namespace ChocolateBackEnd.Controllers;

public class SearchController : BaseApiController
{

    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [HttpGet]
    public async Task<ActionResult<SearchResultResponse>> GetCategories([FromQuery]GlobalSearchRequest searchRequest)
    {
        var result = await _searchService.GlobalSearch(searchRequest.SearchString);
        await Task.Delay(1000);
        var response = result.Adapt<SearchResultResponse>();
        return Ok(response);
    }
}