using System.IO.Compression;
using System.Net.Mime;
using AutoMapper;
using ChocolateDomain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using ChocolateBackEnd.Auth;
using Microsoft.AspNetCore.Authorization;
using Models;
using Models.Category;
using Models.Product;
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
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAllProducts()
    {
        return Ok(await _productService.GetAllProducts());
    }
    
    [HttpGet("Products/{categoryId:Guid}", Name = "GetProductsByCategory")]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProductsByCategory([FromRoute]Guid categoryId)
    {
        return Ok(await _productService.GetProductsByCategory(categoryId));
    }
    
    [HttpGet("Product/{productId:Guid}", Name = "GetProduct")]
    public async Task<ActionResult<ProductDTO>> GetProduct(Guid productId)
    {
        var product = await _productService.GetProductWithPhotoIds(productId);

        return Ok(product);
    }
    
    [HttpGet("Categories", Name = "GetAllCategories")]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        return Ok(await _productService.GetAllCategories());
    }
    
    [HttpGet("Category/{categoryId:guid}", Name = "GetCategory")]
    public async Task<ActionResult<CategoryDTO>> GetCategory([FromRoute] Guid categoryId)
    {
        return Ok(await _productService.GetCategory(categoryId));
    }
    
    [Authorize(Policy = Policies.Admin)]
    [HttpPost("Category", Name = "AddCategory")]
    public async Task<ActionResult<Guid>> AddCategory(CategoryDTO category)
    {
        return Ok(await _productService.AddNewCategory(category));
    }

    [Authorize(Policy = Policies.Admin)]
    [HttpPost("Products", Name = "AddProduct")]
    public async Task<ActionResult<Guid>> AddProduct([FromBody]ProductCreateRequest product)
    {
        var newProductId = await _productService.AddNewProduct(product);
        
        return Ok(newProductId);
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
            
            var newPhotoId = await _photoService.AddPhoto(productId, readStream);

            var product = await _productService.GetProduct(productId);
            if (product.MainPhotoId is null)
            {
                product.MainPhotoId = newPhotoId;
                await _productService.UpdateProduct(product);
            }
            
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

        var stream = await _photoService.GetImage(photoId);

        return File(stream, MediaTypeNames.Image.Jpeg);
    }    

    [HttpGet("Products/{productId:Guid}/Photos", Name = "GetAllPhotos")]
    public async Task<ActionResult> GetPhotos([FromRoute] Guid productId)
    {
        var photos = await _photoService.GetPhotosByProduct(productId);
        
        var streams = photos.Select(async photo => await _photoService.GetImage(photo));

        Stream archiveStream = new MemoryStream();
        var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true);

        var i = 1;
        foreach (Task<Stream> stream in streams)
        {
            var archiveEntry = zipArchive.CreateEntry($"{i}.jpg");
            await using var newEntryStream = archiveEntry.Open();
            await (await stream).CopyToAsync(newEntryStream);
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