using EFCoreMigrations;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ToolController : ControllerBase
{
    private MyDbContext _context;

    public ToolController(MyDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public string InitDateBase()
    {
        //创建初始化值
        Users user = new Users()
        {
            Name = "lbh",
            NickName = "超级管理员",
            Password = "123456",
            UserType = 0,
            IsEnable = true,
            Description = "默认角色",
            CreateDate = DateTime.Now,
            CreateUserId = 0,
            IsDeleted = 0
        };
        FzTbs fzTbs = new FzTbs()
        {
            yf = "1",
            fz =1200,
            sf =120,
            df =50,
            hj =1900,
            cr =321,
            sy =123,
            jldc=123,
            ck =123,
            Description = "默认角色",
            CreateDate = DateTime.Now,
            CreateUserId = 0,
            IsDeleted = 0
        };
        _context.Users.Add(user);
        _context.FzTbs.Add(fzTbs);
        _context.SaveChanges();
        return "ok";
    }
}