using System.Net.Mime;
using ApiContracts.Photo;
using ApiContracts.Product;
using ChocolateDomain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using ChocolateBackEnd.Auth;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Models.Product;
using Services.Photo;
using Services.Product;

namespace ChocolateBackEnd.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IPhotoService _photoService;
    private readonly IProductService _productService;

    public ProductsController(IPhotoService photoService, IProductService productService)
    {
        _photoService = photoService;
        _productService = productService;
    }

   
    [HttpGet("{productId:Guid}", Name = "GetProduct")]
    public async Task<ActionResult<ProductResponse>> GetProduct(Guid productId)
    {
        var product = await _productService.GetProductWithPhotoIds(productId);
        var response = product.Adapt<ProductResponse>();
        return Ok(response);
    }
    
    [HttpGet]
    public async Task<ActionResult<List<ProductResponse>>> GetProductsByCategory([FromQuery]Guid? categoryId)
    {
        await Task.Delay(1000);
        var result = categoryId is null
            ? await _productService.GetAllProducts()
            : await _productService.GetProductsByCategory(categoryId.Value);

        var response = result.Adapt<List<ProductResponse>>();
        return response;
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpPost]
    public async Task<ActionResult<Guid>> AddProduct([FromBody]ProductCreateRequest createProductRequest)
    {
        var newProduct = createProductRequest.Adapt<ProductDto>();
        var newProductId = await _productService.AddNewProduct(newProduct);
        return Ok(newProductId);
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpPut]
    public async Task<ActionResult<Guid>> UpdateProduct([FromBody]ProductDto product)
    {
        await _productService.UpdateProduct(product);
        return Ok();
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpPost("{productId:Guid}/Photos")]
    public async Task<IActionResult> AddPhotos([FromBody]AddPhotoRequest addPhotoRequest)
    {
        try
        {
            var photo = Convert.FromBase64String(addPhotoRequest.PhotoBase64);
            var newPhotoId = await _photoService.AddPhoto(addPhotoRequest.ProductId, photo);

            return Ok(newPhotoId);
        }
        catch (EntityNotFoundException e)
        {
            return BadRequest("Incorrect ProductID");
        }
    }

    [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpDelete("{productId:guid}", Name = "DeleteProduct")]
    public async Task<IActionResult> DeleteProduct([FromRoute]Guid productId)
    {
        await _productService.DeleteProduct(productId);
        return Ok();
    }

    [HttpGet("{productId:Guid}/Photos/{photoId:Guid}")]
    public async Task<ActionResult> GetPhotos([FromRoute] Guid productId, [FromRoute] Guid photoId)
    {
        var photos = (await _photoService.GetPhotosByProductId(productId)).ToArray();
        if (photos is null) throw new PhotoNotFoundException(productId);

        var stream = await _photoService.GetPhoto(photoId);

        return File(stream, MediaTypeNames.Image.Jpeg);
    }    

    // [HttpGet("{productId:Guid}/Photos")]
    // public async Task<ActionResult> GetPhotos([FromRoute] Guid productId)
    // {
    //     var photos = await _photoService.GetPhotosByProductId(productId);
    //     
    //     var streams = photos.Select(async photo => await _photoService.GetPhotosByProductId(photo));
    //
    //     Stream archiveStream = new MemoryStream();
    //     var zipArchive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true);
    //
    //     var i = 1;
    //     foreach (Task<Stream> stream in streams)
    //     {
    //         var archiveEntry = zipArchive.CreateEntry($"{i}.jpg");
    //         await using var newEntryStream = archiveEntry.Open();
    //         await (await stream).CopyToAsync(newEntryStream);
    //         i++;
    //     }
    //     
    //     zipArchive.Dispose();
    //     archiveStream.Seek(0, SeekOrigin.Begin);
    //     return File(archiveStream, MediaTypeNames.Application.Octet, fileDownloadName: "Photos.zip");
    // }
}