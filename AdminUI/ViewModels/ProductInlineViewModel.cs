using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AdminUI.ViewModels;

public class ProductsInlineViewModel 
{
    [DisplayName("Идентификатор")]
    public Guid Id { get; init; }
    
    [DisplayName("Название товара")]
    [MaxLength(50)]
    [MinLength(5)]
    public string Name { get; set; }

    [DisplayName("Описание")]
    public string Description { get; set; }

    [DisplayName("Цена")]
    public decimal Price { get; set; }

    [DisplayName("Основная фотография")]
    public Guid? MainPhotoId { get; set; }

    public List<Guid> Photos { get; set; }

    public Guid CategoryId { get; set; }

    [DisplayName("Категория")]
    public string CategoryName { get; set; }

    [DisplayName("Состав")]
    public string? Composition { get; set; }

    [DisplayName("Вес")]
    public int? Weight { get; set; }

    [DisplayName("Ширина")]
    public decimal? Width { get; set; }

    [DisplayName("Высота")]
    public decimal? Height { get; set; }
}