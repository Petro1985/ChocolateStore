using System.Net.Mime;
using AutoMapper;
using ChocolateBackEnd.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Photo;

namespace ChocolateBackEnd.Controllers;

public class PhotosController : BaseApiController
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
    
    [Authorize(Policy = Policies.Admin)]
    [HttpPost("Crop", Name = "CropPhoto")]
    public async Task<IActionResult> CropPhoto()
    {
        var stream = HttpContext.Request.BodyReader.AsStream();
        var newPhoto = await _photoService.CropPhoto(stream);
        var newPhotoBase64 = Convert.ToBase64String(newPhoto);
        return Ok(newPhotoBase64);
    }
    
    [Authorize(Policy = Policies.Admin)]
    [HttpDelete("{photoId:guid}", Name = "DeletePhoto")]
    public async Task<IActionResult> DeletePhoto([FromRoute]Guid photoId)
    {
        await _photoService.Delete(photoId);
        return Ok();
    }

}