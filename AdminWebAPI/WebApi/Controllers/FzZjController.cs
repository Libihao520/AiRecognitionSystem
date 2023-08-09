using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.Desk;
using Model.Dto.User;
using Model.Entitys;
using Model.Other;
using WebApi.Config;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
// [Authorize]
public class FzZjController :ControllerBase
{
    private IDeskTopService _deskTopService;

    public FzZjController(IDeskTopService deskTopService)
    {
        _deskTopService = deskTopService;
    }

    [HttpGet]
    public  ApiResult desktop()
    {
        
            return ResultHelper.Success( _deskTopService.GetData());
    }
}