using System.Net.Mime;
using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Models;
using Models.Photo;
using Models.Product;

//[assembly:InternalsVisibleTo(assemblyName:"ChocolateBackEnd.Tests")]

namespace Services.Photo;

public class PhotoService : IPhotoService
{
    private readonly PhotoRepository _photoDb;
    private readonly IMapper _mapper;

    public PhotoService(IDbRepository<PhotoEntity> photoDb, IMapper mapper)
    {
        _mapper = mapper;
        _photoDb = (PhotoRepository)photoDb;
    }

    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO productEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> AddPhoto(Guid productId, Stream photo)
    {
        using var buffer = new MemoryStream();
        photo.Position = 0;
        await photo.CopyToAsync(buffer);

        var photoEntity = new PhotoEntity
        {
            ProductId = productId,
            Image = buffer.ToArray(), 
        };
        
        return await _photoDb.Add(photoEntity);
    }

    public async Task Delete(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
        await _photoDb.Delete(photoEntity);
    }

    public async Task Delete(Guid id)
    {
        // var entity = await _photoDb.Get(id);
        // _fileService.DeleteFile(entity);
        await _photoDb.Delete(id);
    }

    public async Task Change(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
        // photoEntity.PathToPhoto = photo.PathToPhoto;
        photoEntity.ProductId = photo.ProductId;
        
        await _photoDb.Change(photoEntity);
    }

    public async Task<Stream> GetImage(Guid id)
    {
        var photoEntity = await _photoDb.Get(id);
        return new MemoryStream(photoEntity.Image);
    }

    public async Task<Stream> GetImage(PhotoDTO photo)
        => await GetImage(photo.Id);

    public async Task<IEnumerable<PhotoDTO>> GetPhotosByProduct(Guid productId)
    {
        return _mapper.Map<IEnumerable<PhotoDTO>>(await _photoDb.GetPhotosByProduct(productId));
    }
    
}