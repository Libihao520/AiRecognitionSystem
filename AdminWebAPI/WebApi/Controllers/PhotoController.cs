using Interface;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.photo;
using Model.Other;
using WebApi.Config;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PhotoController : ControllerBase
{
    private IPhotoService _photoService;


    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    [HttpPut]
    public async Task<ApiResult> PutPhoto(PhotoAdd po)
    {

        return ResultHelper.Success(await _photoService.PutPhoto(po));
    }
}