namespace ApiContracts.Search;

public class SearchResultResponse
{
    public List<ProductsSearchResult> Products { get; set; }
    public List<CategoriesSearchResult> Categories { get; set; }
}

public class CategoriesSearchResult
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid? MainPhotoId { get; set; }
}

public class ProductsSearchResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? MainPhotoId { get; set; }
}