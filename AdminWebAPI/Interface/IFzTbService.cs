using Model.Dto.Desk;
using Model.Entitys;
using Model.Other;

namespace Interface;

public interface IFzTbService
{
    PageInfo GetDeskData();
    PageInfo GetTableData();
}