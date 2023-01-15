using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Entities;

[Table("Categories")]
public class CategoryEntity : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid? MainPhotoId { get; set; }
    public IEnumerable<ProductEntity> Products { get; set; }
}
