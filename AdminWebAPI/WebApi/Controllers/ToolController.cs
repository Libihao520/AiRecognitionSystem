using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Model.Entitys;
using SqlSugar;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ToolController : ControllerBase
{
    public ISqlSugarClient _db;

    public ToolController(ISqlSugarClient sqlSugarClient)
    {
        _db = sqlSugarClient;
    }

    [HttpGet]
    public string InitDateBase()
    {
        string res = "ok";
        //创建数据库
        _db.DbMaintenance.CreateDatabase();
        // 创建表
        string nspace = "Model.Entitys";
        // 通过反射读取类
        Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes()
            .Where(p => p.Namespace == nspace).ToArray();
        _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);
        //初始化数据库
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
        _db.Insertable(user).ExecuteReturnBigIdentity();
        return res;
    }
}