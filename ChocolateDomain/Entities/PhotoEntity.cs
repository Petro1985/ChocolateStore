using System.ComponentModel.DataAnnotations;
using ChocolateDomain.Interfaces;

namespace ChocolateDomain.Entities;

public class PhotoEntity : IEntity
{
    public Guid Id { get; init; }
    
    public byte[] Image { get; set; }

    public Guid ProductId { get; set; }
    
    public ProductEntity Product {get; init; }
}