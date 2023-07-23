using Model.Dto.User;
using Model.Entitys;

namespace Interface;

public interface IUserService
{
    List<Users> GetUser(string userName, string passWord);
}