using System.ComponentModel;
using System.Net.Mime;
using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using Models.Photo;
using Models.Product;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

//[assembly:InternalsVisibleTo(assemblyName:"ChocolateBackEnd.Tests")]

namespace Services.Photo;

public class PhotoService : IPhotoService
{
    private readonly IProductRepository _productDb;
    private readonly IPhotoRepository _photoDb;
    private readonly IMapper _mapper;

    public PhotoService(IProductRepository productDb, IPhotoRepository photoDb, IMapper mapper)
    {
        _productDb = productDb;
        _photoDb = photoDb;
        _mapper = mapper;
    }

    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO productEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> CropPhoto(byte[] photo)
    {
        var loadedImage = Image.Load(photo);
        if (loadedImage is null)
        {
            throw new InvalidEnumArgumentException("Не поддерживаемый тип изображения");
        }

        var xCrop = Math.Min(800, loadedImage.Width);
        var yCrop = Math.Min(800, loadedImage.Height);
        
        var xCropOffset = (loadedImage.Width - xCrop) / 2;
        var yCropOffset  = (loadedImage.Height - yCrop) / 2;
        
        loadedImage.Mutate(x => x.Crop(
            new Rectangle(xCropOffset, yCropOffset, xCrop, yCrop)));

        var stream = new MemoryStream();
        await loadedImage.SaveAsPngAsync(stream);
        return stream.ToArray();
    }

    public async Task<Guid> AddPhoto(Guid productId, byte[] photo)
    {
        
        var photoEntity = new PhotoEntity
        {
            ProductId = productId,
            Image = await CropPhoto(photo), 
        };
        var newPhotoId = await _photoDb.Add(photoEntity);

        var product = await _productDb.Get(productId);
        
        if (product.MainPhotoId == default)
        {
            product.MainPhotoId = newPhotoId;
            await _productDb.Change(product);
        }
        return newPhotoId;
    }

    public async Task Delete(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
        await _photoDb.Delete(photoEntity);
    }

    public async Task Delete(Guid id)
    {
        await _photoDb.Delete(id);
    }

    public async Task Change(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
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