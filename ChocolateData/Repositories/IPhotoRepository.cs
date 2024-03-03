using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;

namespace ChocolateData.Repositories;

public interface IPhotoRepository : IDbRepository<PhotoEntity>
{
    /// <summary>
    /// Возвращает идентификаторы всех фотографий относящихся к товару
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    Task<IEnumerable<Guid>> GetPhotoIdsByProduct(Guid productId);
    
    /// <summary>
    /// Возвращает словарик соответствия Id товара массиву Id фотографий 
    /// </summary>
    /// <param name="productIds"></param>
    /// <returns></returns>
    Task<Dictionary<Guid, IEnumerable<Guid>>> GetPhotoIdsByProductIds(IEnumerable<Guid> productIds);
    
    
    Task<IEnumerable<PhotoEntity>> GetPhotosByProduct(Guid productId);
    
    /// <summary>
    /// Возвращает фотограцию по идентификатору
    /// </summary>
    /// <param name="photoId"></param>
    /// <returns></returns>
    Task<byte[]> GetPhoto(Guid photoId);
    
    /// <summary>
    /// Возвращает уменьшеную копия фотограции по Id
    /// </summary>
    /// <param name="photoId"></param>
    /// <returns></returns>
    Task<byte[]?> GetThumbnail(Guid photoId);
}