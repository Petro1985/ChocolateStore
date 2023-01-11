using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Models;
using Services.File;

//[assembly:InternalsVisibleTo(assemblyName:"ChocolateBackEnd.Tests")]

namespace Services.Photo;

public class PhotoService : IPhotoService
{
    private readonly IFileService _fileService;
    private readonly PhotoRepository _photoDb;
    private readonly IMapper _mapper;

    public PhotoService(IFileService fileService, IDbRepository<PhotoEntity> photoDb, IMapper mapper)
    {
        _fileService = fileService;
        _mapper = mapper;
        _photoDb = (PhotoRepository)photoDb;
    }

    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO productEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> AddPhoto(Guid productId, Stream photo)
    {
        var filePath = await _fileService.SaveFile(photo);
        var photoEntity = new PhotoEntity(filePath, productId);
        return await _photoDb.Add(photoEntity);
    }

    public async Task Delete(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
        await _photoDb.Delete(photoEntity);
    }

    public async Task Delete(Guid id)
    {
        var entity = await _photoDb.Get(id);
        _fileService.DeleteFile(entity.PathToPhoto);
        await _photoDb.Delete(id);
    }

    public async Task Change(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
        photoEntity.PathToPhoto = photo.PathToPhoto;
        photoEntity.ProductId = photo.ProductId;
        
        await _photoDb.Change(photoEntity);
    }

    public async Task<Stream> Get(Guid id)
    {
        var photoEntity = await _photoDb.Get(id);
        return await _fileService.LoadFile(photoEntity.PathToPhoto);
    }

    public async Task<Stream> GetPhotoFile(PhotoDTO photoEntity)
    {
        return await _fileService.LoadFile(photoEntity.PathToPhoto);
    }

    public async Task<IEnumerable<PhotoDTO>> GetPhotosByProduct(Guid productId)
    {
        return _mapper.Map<IEnumerable<PhotoDTO>>(await _photoDb.GetPhotosByProduct(productId));
    }
    
}