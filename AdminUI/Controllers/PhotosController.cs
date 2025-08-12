using System.Net.Mime;
using ApiContracts.Photo;
using AutoMapper;
using ChocolateDomain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Services.Photo;

namespace AdminUI.Controllers;

[Route("Photos")]
public class PhotosController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public PhotosController(IMapper mapper, IPhotoService photoService)
    {
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet("{photoId:Guid}")]
    public async Task<ActionResult<IFormFile>> GetPhoto([FromRoute]Guid photoId)
    {
        var stream = await _photoService.GetPhoto(photoId);
        return File(stream, MediaTypeNames.Image.Jpeg);        
    }
    
    [HttpGet("Thumbnail/{photoId:Guid}")]
    public async Task<ActionResult<IFormFile>> GetThumbnail([FromRoute]Guid photoId)
    {
        var stream = await _photoService.GetThumbnail(photoId);
        return File(stream, MediaTypeNames.Image.Jpeg);        
    }
    
    // [Authorize(Policy = PoliciesConstants.Admin)]
    [HttpDelete("{photoId:guid}", Name = "DeletePhoto")]
    public async Task<IActionResult> DeletePhoto([FromRoute]Guid photoId)
    {
        await _photoService.Delete(photoId);
        return Ok();
    }
    
    [HttpPost("/Product/{productId:Guid}/Photos")]
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
}