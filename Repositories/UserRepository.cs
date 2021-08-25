using System.Collections.Generic;
using System.Linq;
using Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Blog.Repositories
{
    public class UserRepository : Repository<User>
    {
        private readonly SqlConnection _connection;
        public UserRepository(SqlConnection connection) : base(connection) => _connection = connection;

        public List<User> GetWithRoles()
        {
            var query = @"SELECT [User].*, [Role].* 
                FROM [User] 
                LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

            var users = new List<User>();
            var items = _connection.Query<User, Role, User>(query,
            (user, role) =>
            {
                var _user = users.FirstOrDefault(x => x.Id == user.Id);
                if (_user == null)
                {
                    _user = user;
                    if (role != null)
                        _user.Roles.Add(role);
                    users.Add(_user);
                }
                else
                {
                    if (role != null)
                        _user.Roles.Add(role);
                }
                return _user;
            }, splitOn: "Id");

            return users;
        }
    }
}