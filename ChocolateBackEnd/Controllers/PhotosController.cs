using ChocolateData;
using ChocolateDomain;
using ChocolateDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChocolateBackEnd.Controllers;

[Authorize]
[ApiController]
public class PhotosController : ControllerBase
{
    private readonly FileService _fileService;
    private readonly IDbRepository<Product> _productDb;
    private readonly IDbRepository<Photo> _photoDb;

    public PhotosController(FileService fileService, IDbRepository<Product> productDb, IDbRepository<Photo> photoDb)
    {
        _fileService = fileService;
        _productDb = productDb;
        _photoDb = photoDb;
    }
}