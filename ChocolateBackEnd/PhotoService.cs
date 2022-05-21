using System.Runtime.CompilerServices;
using ChocolateData;
using ChocolateData.Repositories;
using ChocolateDomain;
using ChocolateDomain.Interfaces;

//[assembly:InternalsVisibleTo(assemblyName:"ChocolateBackEnd.Tests")]

namespace ChocolateBackEnd;

public class PhotoService
{
    private readonly FileService _fileService;
    private readonly PhotoRepository _photoDb;


    public PhotoService(FileService fileService, IDbRepository<Photo> photoDb)
    {
        _fileService = fileService;
        _photoDb = (PhotoRepository)photoDb;
    }
    
    public async Task<long> Add(Product product, Stream fileStream)
    {
        var filePath = await _fileService.SaveFile(fileStream);
        var photo = new Photo(filePath, product);
        return await _photoDb.Add(photo);
    }

    public async Task Delete(Photo entity)
    {
        _fileService.DeleteFile(entity);
        await _photoDb.Delete(entity);
    }

    public async Task Delete(long id)
    {
        var entity = await _photoDb.Get(id);
        _fileService.DeleteFile(entity);
        await _photoDb.Delete(id);
    }

    public async Task Change(Photo entity)
    {
        await _photoDb.Change(entity);
    }

    public async Task<Stream> Get(long id)
    {
        return await _fileService.LoadFile(await _photoDb.Get(id));
    }

    public async Task<Stream> GetPhotoFile(Photo photo)
    {
        return await _fileService.LoadFile(photo);
    }

    public IQueryable<Photo> GetQuery()
    {
        return _photoDb.GetQuery();
    }
    
    public async Task<IEnumerable<Photo>> GetPhotosByProduct(long productId)
    {
        return await _photoDb.GetPhotosByProduct(productId);
    }
}