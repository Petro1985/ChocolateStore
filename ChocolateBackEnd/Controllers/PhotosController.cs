using ChocolateData;
using ChocolateDomain;
using ChocolateDomain.Entities;
using ChocolateDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.File;

namespace ChocolateBackEnd.Controllers;

[Authorize]
[ApiController]
public class PhotosController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IDbRepository<ProductEntity> _productDb;
    private readonly IDbRepository<PhotoEntity> _photoDb;

    public PhotosController(IFileService fileService, IDbRepository<ProductEntity> productDb, IDbRepository<PhotoEntity> photoDb)
    {
        _fileService = fileService;
        _productDb = productDb;
        _photoDb = photoDb;
    }
}