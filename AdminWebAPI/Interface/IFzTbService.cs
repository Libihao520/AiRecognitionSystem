using Model.Dto.Desk;
using Model.Entitys;
using Model.Other;

namespace Interface;

public interface IFzTbService
{
    /// <summary>
    /// 获取桌面数据
    /// </summary>
    /// <returns></returns>
    PageInfo GetDeskData();
    /// <summary>
    /// 获取表单数据
    /// </summary>
    /// <returns></returns>
    PageInfo GetTableData();
    /// <summary>
    /// 添加表单数据
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    PageInfo AddTableData(FzTbAdd db);
    /// <summary>
    /// 修改表单数据
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    PageInfo PutTableData(FzTbEdit db);
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    PageInfo DelTableData(long id);
}