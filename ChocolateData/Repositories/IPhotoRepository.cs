﻿using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;

namespace ChocolateData.Repositories;

public interface IPhotoRepository : IDbRepository<PhotoEntity>
{
    Task<IEnumerable<Guid>> GetPhotoIdsByProduct(Guid productId);
    Task<IEnumerable<PhotoEntity>> GetPhotosByProduct(Guid productId);
    Task<byte[]> GetPhoto(Guid photoId);
    Task<byte[]?> GetThumbnail(Guid photoId);
}