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

    public async Task<byte[]> CropPhoto(Stream photo)
    {
        var loadedImage = await Image.LoadAsync(photo);
        if (loadedImage is null)
        {
            throw new InvalidEnumArgumentException("Не поддерживаемый формат изображения");
        }

        double width = loadedImage.Width;
        double height = loadedImage.Height;

        var newWidth = 800;
        var newHeight = 800;
        
        var xCropOffset = 0;
        var yCropOffset  = 0;

        if (width > height)
        {
            newWidth = (int)Math.Floor(width / (height / 800));
            xCropOffset = (newWidth - 800) / 2;
        }
        else
        {
            newHeight = (int)Math.Floor(height / (width / 800));
            yCropOffset  = (newHeight - 800) / 2;
        }
        
        loadedImage.Mutate(x =>
        { 
            x
                .Resize(newWidth, newHeight)
                .Crop(new Rectangle(xCropOffset, yCropOffset, Math.Min(800, newWidth), Math.Min(800, newHeight)))
                .AutoOrient();
        });

        var stream = new MemoryStream();
        await loadedImage.SaveAsPngAsync(stream);
        return stream.ToArray();
    }

    public async Task<Guid> AddPhoto(Guid? productId, byte[] photo)
    {
        
        var photoEntity = new PhotoEntity
        {
            ProductId = productId,
            Image = await CropPhoto(new MemoryStream(photo)), 
        };
        var newPhotoId = await _photoDb.Add(photoEntity);
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
        
        await _photoDb.Update(photoEntity);
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