using AutoMapper;
using EFCoreMigrations;
using Interface;
using Model.Dto.Desk;
using Model.Entitys;
using Model.Other;

namespace Service;

public class FzTbService : IFzTbService
{
    private readonly IMapper _mapper;
    private MyDbContext _context;

    public FzTbService(MyDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public PageInfo GetDeskData()
    {
        PageInfo pageInfo = new PageInfo();
        var deskTopsEnumerable = _context.FzTbs.ToList();

        var totals = _context.FzTbs
            .GroupBy(d => 1)
            .Select(g => new
            {
                cr = g.Sum(x => x.cr),
                jldc = g.Sum(x => x.jldc)
            }).First();

        if (deskTopsEnumerable != null)
        {
            var deskRes = new DeskRes();
            deskRes.cr = totals.cr;
            deskRes.jldc = totals.jldc;
            pageInfo.Total = 1;
            pageInfo.Data = deskRes;
            return pageInfo;
        }

        return new PageInfo();
    }

    public PageInfo GetTableData()
    {
        PageInfo pageInfo = new PageInfo();
        var deskTopsEnumerable = _context.FzTbs.ToList();
        if (deskTopsEnumerable != null)
        {
            pageInfo.Total = 1;
            pageInfo.Data = _mapper.Map<List<FzTbRes>>(deskTopsEnumerable);
            return pageInfo;
        }

        return new PageInfo();
    }

    public PageInfo AddTableData(FzTbAdd db)
    {
        if (db != null)
        {
            PageInfo pageInfo = new PageInfo();
            var deskTop = _mapper.Map<FzTbs>(db);
            deskTop.Description = "默认角色";
            deskTop.CreateDate = DateTime.Now;
            deskTop.CreateUserId = 0;
            deskTop.IsDeleted = 0;

            _context.FzTbs.Add(deskTop);
            _context.SaveChanges();
            
            return new PageInfo();
        }

        return new PageInfo();
    }
}