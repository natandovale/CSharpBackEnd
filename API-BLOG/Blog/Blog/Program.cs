using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using System;
using System.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string _connection = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";

        public static void Main(string[] args)
        {
            var connection = new SqlConnection(_connection);
            //var repository = new Repository<User>(connection);
            //var users = repository.Get();
            //foreach (var item in users)
            //{
            //    Console.WriteLine(item.Name);
            //}
            connection.Open();
            Load();
            Console.ReadKey();
            connection.Close();
        }

        private static void Load()
        {

        }
    }
}
