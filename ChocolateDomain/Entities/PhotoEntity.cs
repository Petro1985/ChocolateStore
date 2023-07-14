using System.ComponentModel.DataAnnotations;
using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Entities;

public class PhotoEntity : IEntity
{
    public Guid Id { get; init; }
    
    /// <summary>
    /// Фотография
    /// </summary>
    public byte[] Image { get; set; }

    /// <summary>
    /// Миниатюра фотографии
    /// </summary>
    public byte[]? Thumbnail { get; set; }

    public Guid? ProductId { get; set; }
    
    public virtual ProductEntity Product {get; init; }
}