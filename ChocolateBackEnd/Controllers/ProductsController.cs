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
using ChocolateBackEnd.Auth;
using Microsoft.AspNetCore.Authorization;
using Models;
using Services.Photo;
using Services.Product;

namespace ChocolateBackEnd.Controllers;

[ApiController]
public class ProductsController : Controller
{
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;
    private readonly IProductService _productService;
    
    public ProductsController(IMapper mapper,  IPhotoService photoService, IProductService productService)
    {
        _mapper = mapper;
        _photoService = photoService;
        _productService = productService;
    }

    [HttpGet("Products", Name = "GetAllProducts")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        return Ok(await _productService.GetAllProducts());
    }
    
    [Authorize(Policy = Policies.Admin)]
    [HttpPost("Products", Name = "Post")]
    public async Task<ActionResult<Guid>> Add(ProductDTO product)
    {
        return Ok(await _productService.AddNewProduct(product));
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpPost("Products/{productId:Guid}/Photos", Name = "PhotoPost")]
    public async Task<IActionResult> AddPhotos([FromRoute]Guid productId, IFormFile photo)
    {
        try
        {
            await using var readStream = photo.OpenReadStream();
            if (!IsImage(readStream))
            {
                return BadRequest("Sent file isn't image.");                
            }
            
            await _photoService.AddPhoto(productId, readStream);
            return Ok();
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest("Incorrect ProductID");
        }
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpDelete("Products", Name = "DeleteProduct")]
    public async Task<IActionResult> DeleteProduct([FromQuery]Guid productId)
    {
        // try
        // {
        //     var photos = await _photoService.GetPhotosByProduct(productId);
        //     foreach (var photo in photos)
        //     {
        //         await _photoService.Delete(photo);
        //     }
        //     await _productDb.Delete(productId);
        //     return Ok();
        // }
        // catch (Exception e)
        // {
        //     return BadRequest(e.Message);
        // }
        return BadRequest("e.Message");
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpDelete("Products/{productId:Guid}/Photos/{photoId:Guid}", Name = "DeletePhoto")]
    public async Task<IActionResult> DeletePhoto([FromRoute]Guid productId, [FromRoute]Guid photoId)
    {
        var photos = (await _photoService.GetPhotosByProduct(productId)).ToArray();
        if (photos is null) throw new PhotoNotFoundException(productId);

        var photoToDelete = photos.FirstOrDefault(x => x.Id == photoId);
        if (photoToDelete is null) return BadRequest($"There isn't photo with Id {photoToDelete.Id}");
        
        await _photoService.Delete(photoToDelete);
        return Ok();
    }
    
    [HttpGet("Products/{productId:Guid}/Photos/{photoId:Guid}", Name = "GetPhoto")]
    public async Task<ActionResult> GetPhotos([FromRoute] Guid productId, [FromRoute] Guid photoId)
    {
        var photos = (await _photoService.GetPhotosByProduct(productId)).ToArray();
        if (photos is null) throw new PhotoNotFoundException(productId);

        var photo = photos.FirstOrDefault(x => x.Id == photoId);
        if (photo is null) return BadRequest($"There isn't photo with Id {photoId.ToString()}");

        var stream = await _photoService.GetPhotoFile(photo);

        return File(stream, MediaTypeNames.Image.Jpeg);
    }    

    [HttpGet("Products/{productId:Guid}/Photos", Name = "GetAllPhotos")]
    public async Task<ActionResult> GetPhotos([FromRoute] Guid productId)
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