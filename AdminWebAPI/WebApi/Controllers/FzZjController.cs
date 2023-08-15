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
    private IFzTbService _fzTbService;

    public FzZjController(IFzTbService fzTbService)
    {
        _fzTbService = fzTbService;
    }

    [HttpGet]
    public  ApiResult GetDesktop()
    {
        return ResultHelper.Success( _fzTbService.GetDeskData());
    }
    
    [HttpGet]
    public  ApiResult GetTable()
    {
        
        return ResultHelper.Success( _fzTbService.GetTableData());
    }

    [HttpPost]
    public ApiResult AddTable(FzTbAdd db)
    {
        return ResultHelper.Success(_fzTbService.AddTableData(db));
    }
    [HttpPut]
    public ApiResult PutTable(FzTbEdit db)
    {
        return ResultHelper.Success(_fzTbService.PutTableData(db));
    }
    [HttpDelete]
    public ApiResult DelTable(long id)
    {
        return ResultHelper.Success(_fzTbService.DelTableData(id));
    }
}