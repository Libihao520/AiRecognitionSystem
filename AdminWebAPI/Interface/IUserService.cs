using Model.Dto.User;

namespace Interface;

public interface IUserService
{
    UserRes GetUser(string userName, string passWord);
}