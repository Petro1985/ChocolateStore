namespace AdminUI.ViewModels;

public class CategoryInlineViewModel 
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid? MainPhotoId { get; set; }
}