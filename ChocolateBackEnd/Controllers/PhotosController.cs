using System.Net.Mime;
using AutoMapper;
using ChocolateData;
using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Photo;

namespace ChocolateBackEnd.Controllers;

[ApiController]
public class PhotosController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPhotoService _photoService;

    public PhotosController(IMapper mapper, IPhotoService photoService)
    {
        _mapper = mapper;
        _photoService = photoService;
    }

    [HttpGet("/image/{photoId:Guid}")]
    public async Task<ActionResult<IFormFile>> GetImage([FromRoute]Guid photoId)
    {
        var stream = await _photoService.GetImage(photoId);
        return File(stream, MediaTypeNames.Image.Jpeg);        
    }
}