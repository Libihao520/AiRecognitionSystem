using AutoMapper;
using EFCoreMigrations;
using Interface;
using Microsoft.EntityFrameworkCore;
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
            var fzTb = _mapper.Map<FzTbs>(db);
            fzTb.Description = "默认角色";
            fzTb.CreateDate = DateTime.Now;
            fzTb.CreateUserId = 0;
            fzTb.IsDeleted = 0;
            fzTb.hj = fzTb.fz+fzTb.sf + fzTb.df;
            fzTb.sy = fzTb.cr - fzTb.hj;
            fzTb.ck = fzTb.sy - fzTb.jldc;
            _context.FzTbs.Add(fzTb);
            _context.SaveChanges();

            return new PageInfo();
        }

        return new PageInfo();
    }

    public PageInfo PutTableData(FzTbEdit db)
    {
        if (db != null)
        {
            var fztb = _context.FzTbs.AsNoTracking().FirstOrDefault(x => x.Id == db.Id);

            if (fztb != null)
            {
                var fzTb = _mapper.Map<FzTbs>(db);
                fzTb.Description = "默认角色";
                fzTb.CreateDate = DateTime.Now;
                fzTb.CreateUserId = 0;
                fzTb.IsDeleted = 0;
                fzTb.hj = fzTb.fz+fzTb.sf + fzTb.df;
                fzTb.sy = fzTb.cr - fzTb.hj;
                fzTb.ck = fzTb.sy - fzTb.jldc;
                _context.FzTbs.Update(fzTb);
                _context.SaveChanges();
                return new PageInfo();
            }
            return new PageInfo();
        }

        return new PageInfo();
    }
}