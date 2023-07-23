using AutoMapper;
using EFCoreMigrations;
using Interface;
using Model.Dto.Desk;

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

    public DeskReq GetData()
    {
        var deskTopsEnumerable = _context.DeskTops.FirstOrDefault();
        if (deskTopsEnumerable != null)
        {
            return _mapper.Map<DeskReq>(deskTopsEnumerable);
        }
        return new DeskReq();
    }
}