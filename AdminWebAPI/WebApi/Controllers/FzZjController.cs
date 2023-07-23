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
[Authorize]
public class FzZjController :ControllerBase
{
    private IDeskTopService _deskTopService;

    public FzZjController(IDeskTopService deskTopService)
    {
        _deskTopService = deskTopService;
    }

    [HttpGet]
    public  async Task<DeskReq> desktop()
    {
        var res = Task.Run(() =>
        {
            DeskReq deskReq = _deskTopService.GetData();
            return deskReq;
        });
        return await res;
    }
}