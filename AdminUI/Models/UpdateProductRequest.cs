namespace AdminUI.Models;

public class UpdateProductRequest
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public int TimeToMakeInHours { get; set; }
    public Guid MainPhotoId { get; set; }
    
    public Guid CategoryId { get; set; }
    public string? Composition { get; set; }
    public int? Weight { get; set; }
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }

    public IEnumerable<Guid>? Photos { get; set; }
}