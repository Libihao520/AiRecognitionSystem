using System.Data;
using AutoMapper;
using Dapper;
using Interface;
using Microsoft.Data.SqlClient;
using Model.Dto.User;
using Model.Entitys;
using MySqlConnector;
using SqlSugar;

namespace Service;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private ISqlSugarClient _db;

    public UserService(IMapper mapper, ISqlSugarClient db)
    {
        _mapper = mapper;
        _db = db;
    }

    public List<Users> GetUser(string userName, string passWord)
    {
        using IDbConnection connection = new SqlConnection("Data Source=127.0.0.1;Initial Catalog=newlbhadmin;User ID=sa;Password=qwe20211114.;");
        
        // var user = _db.Queryable<Users>().Where(u => u.Name == userName && u.Password == passWord).First();
        
        string sql = "select * from Users ";
        
        return connection.Query<Users>(sql).ToList();

    }
}