using AutoMapper;
using EFCoreMigrations;
using Interface;
using Model.Dto.User;
using Model.Entitys;

namespace Service;

public class UserService : IUserService
{
    private readonly IMapper _mapper;

    private MyDbContext _context;

    public UserService(IMapper mapper, MyDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public UserRes GetUser(string userName, string passWord)
    {
        var users = _context.Users.Where(u => u.Name == userName && u.Password == passWord).FirstOrDefault();
        if (users != null)
        {
            return _mapper.Map<UserRes>(users);
        }

        return new UserRes();
    }
}