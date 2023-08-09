using AutoMapper;
using EFCoreMigrations;
using Interface;
using Model.Dto.Desk;
using Model.Entitys;
using Model.Other;

namespace Service;

public class DeskTopService:IDeskTopService
{
    private readonly IMapper _mapper;
    private MyDbContext _context;

    public DeskTopService(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PageInfo GetData()
    {
        PageInfo pageInfo = new PageInfo();
        var deskTopsEnumerable = _context.DeskTops.ToList();
        if (deskTopsEnumerable != null)
        {
            pageInfo.Total = 1;
            pageInfo.Data = _mapper.Map<List<DeskReq>>(deskTopsEnumerable);
            return pageInfo;
        }
        return new PageInfo();
    }
}