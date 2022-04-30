using System.IO.Compression;
using System.Net.Mime;
using AutoMapper;
using ChocolateBackEnd.APIStruct;
using ChocolateData;
using ChocolateDomain;
using ChocolateDomain.Exceptions;
using ChocolateDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ChocolateBackEnd.Controllers;

[ApiController]
public class ProductsController : Controller
{
    private readonly IMapper _mapper;
    // private readonly FileService _fileService;
    // private readonly PhotoRepository _photoDb;
    private readonly PhotoService _photoService;
    
    private readonly IDbRepository<Product> _productDb;


    public ProductsController(IMapper mapper, IDbRepository<Product> productDb,  PhotoService photoService)
    {
        _mapper = mapper;
        _productDb = productDb;
        _photoService = photoService;
    }

    [HttpGet("Products", Name = "GetAllProducts")]
    public async Task<IEnumerable<ProductResponse>> GetProducts()
    {
        return await _mapper.ProjectTo<ProductResponse>(_productDb.GetQuery()).ToListAsync();
    }
    
    [HttpPost("Products", Name = "Post")]
    public async Task<ProductResponse> Add(ProductAddRequest request)
    {
        var product = new Product(
            request.Description, request.PriceRub, TimeSpan.FromHours(request.TimeToMakeInHours));
        
        await _productDb.Add(product);
        var mappedTask = _mapper.Map<ProductResponse>(product);
        return mappedTask;
    }

    [HttpPost("Products/{productId:long}/Photos", Name = "PhotoPost")]
    public async Task<IActionResult> AddPhotos([FromRoute]long productId, IFormFile photo)
    {
        try
        {
            var product = await _productDb.Get(productId);
        
            await using Stream readStream = photo.OpenReadStream();
            if (!IsImage(readStream))
            {
                return BadRequest("Sent file isn't image.");                
            }
            
            await _photoService.Add(product, readStream);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest("Incorrect ProductID");
        }
    }

    [HttpDelete("Products", Name = "DeleteProduct")]
    public async Task<IActionResult> DeleteProduct([FromBody]ProductDeleteRequest productId)
    {
        try
        {
            var photos = await _photoService.GetPhotosByProduct(productId.Id);
            foreach (Photo photo in photos)
            {
                await _photoService.Delete(photo);
            }
            await _productDb.Delete(productId.Id);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Products/{ProductId:long}/Photos/{PhotoNumber:long}", Name = "DeletePhoto")]
    public async Task<IActionResult> DeleteProduct([FromRoute]PhotoDeleteRequest photoDeleteRequest)
    {
        var photos = (await _photoService.GetPhotosByProduct(photoDeleteRequest.ProductId)).ToArray();
        if (photos is null) throw new PhotoNotFoundException(photoDeleteRequest.ProductId);

        var photoNumber = photoDeleteRequest.PhotoNumber;
        
        var photo = photos.ElementAtOrDefault(photoDeleteRequest.PhotoNumber);
        if (photo is null) return BadRequest($"There isn't photo under number {photoNumber.ToString()}. Count of photos for product {photoDeleteRequest.ProductId.ToString()} is {photos.Count().ToString()}");
        
        await _photoService.Delete(photo);
        return Ok();
    }
    
    [HttpGet("Products/{productId:long}/Photos/{photoNumber:long}", Name = "GetPhoto")]
    public async Task<ActionResult> GetPhotos([FromRoute] long productId, [FromRoute] int photoNumber)
    {
        var photos = (await _photoService.GetPhotosByProduct(productId)).ToArray();
        if (photos is null) throw new PhotoNotFoundException(productId);

        var photo = photos.ElementAtOrDefault(photoNumber);
        if (photo is null) return BadRequest($"There isn't photo under number {photoNumber.ToString()}. Count of photos for product {productId.ToString()} is {photos.Count().ToString()}");

        var stream = await _photoService.GetPhotoFile(photo);

        return File(stream, MediaTypeNames.Image.Jpeg);
    }    

    [HttpGet("Products/{productId:long}/Photos", Name = "GetAllPhotos")]
    public async Task<ActionResult> GetPhotos([FromRoute] long productId)
    {
        var photos = await _photoService.GetPhotosByProduct(productId);
        var streams = photos.Select(async photo => await _photoService.GetPhotoFile(photo)).ToArray();

        Stream archiveStream = new MemoryStream();
        var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true);

        var i = 1;
        foreach (Task<Stream> stream in streams)
        {
            var archiveEntry = zipArchive.CreateEntry($"{i}.jpg");
            await using var newEntryStream = archiveEntry.Open();
            (await stream).CopyTo(newEntryStream);
            i++;
        }
        
        zipArchive.Dispose();
        archiveStream.Seek(0, SeekOrigin.Begin);
        return File(archiveStream, MediaTypeNames.Application.Octet, fileDownloadName: "Photos.zip");
    }

    private bool IsImage(Stream stream)
    {
        try
        {
            Image.FromStream(stream);
            return true;
        }
        catch (ArgumentException _)
        {
            return false;
        }
    }
}