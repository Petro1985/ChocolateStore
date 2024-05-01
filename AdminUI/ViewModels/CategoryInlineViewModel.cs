using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminUI.ViewModels;

public class CategoryInlineViewModel 
{
    [DisplayName("Идентификатор")]
    public Guid Id { get; init; }
    
    [DisplayName("Название категории")]
    [MaxLength(50)]
    [MinLength(5)]
    public string Name { get; set; }

    [DisplayName("Основное фото")]
    public Guid? MainPhotoId { get; set; }
}