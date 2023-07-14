using System.ComponentModel;
using AutoMapper;
using ChocolateData.Repositories;
using ChocolateDomain.Entities;
using Microsoft.Extensions.Options;
using Models.Photo;
using Models.Product;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Services.Photo;

public class PhotoService : IPhotoService
{
    private readonly IPhotoRepository _photoDb;
    private readonly IMapper _mapper;
    private readonly PhotoServiceOptions _options;

    public PhotoService(IPhotoRepository photoDb, IMapper mapper, IOptions<PhotoServiceOptions> options)
    {
        _photoDb = photoDb;
        _mapper = mapper;
        _options = options.Value;
    }

    public Task<IEnumerable<PhotoDTO>> GetPhotos(ProductDTO productEntity)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> CropPhoto(Stream photo)
    {
        var loadedImage = await GetPhoto(photo);
        return await CropImage(loadedImage, _options.BigPhotoSize, _options.BigPhotoSize);
    }

    /// <summary>
    /// Обрезает картинку под маленький размер.
    /// </summary>
    /// <param name="photo">Стрим с картинкой</param>
    /// <returns>массив байт содержащий преобразованную рартинку</returns>
    private async Task<byte[]> CropPhotoToMiniature(Stream photo)
    {
        var loadedImage = await GetPhoto(photo);
        return await CropImage(loadedImage, _options.SmallPhotoSize, _options.SmallPhotoSize);
    }

    private async Task<Image> GetPhoto(Stream photo)
    {
        var loadedImage = await Image.LoadAsync(photo);
        if (loadedImage is null)
        {
            throw new InvalidEnumArgumentException("Не поддерживаемый формат изображения");
        }

        return loadedImage;
    }

    private async Task<byte[]> CropImage(Image image, int newWidth, int newHeight)
    {
        double width = image.Width;
        double height = image.Height;
        
        var xCropOffset = 0;
        var yCropOffset  = 0;

        if (width > height)
        {
            width = (int)Math.Floor(width / (height / newHeight));
            height = newHeight;
            xCropOffset = ((int)width - newWidth) / 2;
        }
        else
        {
            height = (int)Math.Floor(height / (width / newWidth));
            width = newWidth;
            yCropOffset  = ((int)height - newHeight) / 2;
        }
        
        image.Mutate(x =>
        {
            x
                .Resize((int)width, (int)height)
                .Crop(new Rectangle(xCropOffset, yCropOffset, newWidth, newHeight))
                .AutoOrient();
        });

        var stream = new MemoryStream();
        await image.SaveAsJpegAsync(stream);
        return stream.ToArray();
    }
    
    
    
    public async Task<Guid> AddPhoto(Guid? productId, byte[] photo)
    {
        var fullSizeImage = await CropPhoto(new MemoryStream(photo));
        var thumbnailImage = await CropPhotoToMiniature(new MemoryStream(fullSizeImage));
        
        var photoEntity = new PhotoEntity
        {
            ProductId = productId,
            Image = fullSizeImage, 
            Thumbnail = thumbnailImage, 
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

    public async Task UpdatePhoto(PhotoDTO photo)
    {
        var photoEntity = await _photoDb.Get(photo.Id);
        photoEntity.ProductId = photo.ProductId;
        
        await _photoDb.Update(photoEntity);
    }

    public Task<Stream> GetPhoto(PhotoDTO photo)
    {
        throw new NotImplementedException();
    }

    public async Task<Stream> GetPhoto(Guid id)
    {
        var photo = await _photoDb.GetPhoto(id);
        return new MemoryStream(photo);
    }

    public async Task<Stream> GetThumbnail(Guid photoId)
    {
        var thumbnail = await _photoDb.GetThumbnail(photoId);
        if (thumbnail is null)
        {
            var photo = await _photoDb.Get(photoId);
            thumbnail = await CropPhotoToMiniature(new MemoryStream(photo.Image));
            photo.Thumbnail = thumbnail;
            await _photoDb.Update(photo);
        }
        return new MemoryStream(thumbnail);
    }

    public async Task<IEnumerable<PhotoDTO>> GetPhotosByProductId(Guid productId)
    {
        return _mapper.Map<IEnumerable<PhotoDTO>>(await _photoDb.GetPhotosByProduct(productId));
    }
    
}