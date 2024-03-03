using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Entities;

public class ProductEntity : IEntity
{
    public Guid Id { get; init; }
    
    /// <summary>
    /// Имя продукта
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Основное описание продукта
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Цена в рублях
    /// </summary>
    public decimal Price { get; set; }
    
    /// <summary>
    /// Время необходимое для изготовления
    /// </summary>
    public TimeSpan TimeToMake { get; set; }
    
    /// <summary>
    /// Главная фотография продукта
    /// </summary>
    public Guid? MainPhotoId { get; set; }
    public PhotoEntity? MainPhoto { get; set; }
    
    /// <summary>
    /// Категория продукта
    /// </summary>
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }

    /// <summary>
    /// Состав
    /// </summary>
    public string? Composition { get; set; }
    
    /// <summary>
    /// Вес в граммах
    /// </summary>
    public int? Weight { get; set; }

    /// <summary>
    /// Габаритные размеры в сантиметрах
    /// </summary>
    public decimal? Width { get; set; }
    public decimal? Height { get; set; }
    
    public IEnumerable<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();
}