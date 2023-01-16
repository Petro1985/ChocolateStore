using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata;
using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Entities;

public class ProductEntity : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal PriceRub { get; set; }
    public TimeSpan TimeToMake { get; set; }
    public Guid? MainPhotoId { get; set; }
    public PhotoEntity? MainPhoto { get; set; }
    public virtual IEnumerable<PhotoEntity> Photos { get; set; } = new List<PhotoEntity>();
    [Column("CategoryId")]
    public Guid CategoryId { get; set; }
    public virtual CategoryEntity Category { get; set; }
}