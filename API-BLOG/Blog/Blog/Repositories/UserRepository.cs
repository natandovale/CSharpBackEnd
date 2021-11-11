using Blog.Models;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repositories
{
    class UserRepository
    {
        private SqlConnection _connection = new SqlConnection(@"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$");
        public IEnumerable<User> Get()
            => _connection.GetAll<User>();
        

        public User Get(int id)
            => _connection.Get<User>(id);
        

        public void Create(User user)
            => _connection.Insert<User>(user);
    }
}
