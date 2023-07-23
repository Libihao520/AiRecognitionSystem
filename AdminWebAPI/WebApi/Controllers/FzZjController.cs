using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.Dto.User;
using Model.Other;
using WebApi.Config;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class FzZjController :ControllerBase
{
    [HttpGet]
    public  async Task<ApiResult> fzzj()
    {
        var res = Task.Run(() =>
        {
            return ResultHelper.Success("成功");
        });
        return await res;
    }
}