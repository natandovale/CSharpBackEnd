using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using System;
using System.Data.SqlClient;

namespace Blog
{
    class Program
    {
        
        public static void Main(string[] args)
        {
            
            var repository = new UserRepository();
            var users = repository.Get();
            foreach (var item in users)
            {
                Console.WriteLine(item.Name);
            }
            
        }

        public static void ReadUsers(SqlConnection connection)
        {
            using (connection)
            {
                var users = connection.GetAll<User>();
                foreach (var user in users)
                {
                    Console.WriteLine(user.Name);
                }
            }
        }

        public static void ReadUser(SqlConnection connection)
        {
            using (connection)
            {
                var user = connection.Get<User>(1);
               
                Console.WriteLine(user.Name);
            }
        }

        public static void CreateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Bio = "Equipe natan.io",
                Email = "hello@natan.io",
                Image = "https://...",
                Name = "Equipe natan.io",
                PasswordHash = "HASH",
                Slug = "equipe-natan"
            };
            using (connection)
            {
                connection.Insert<User>(user);

                Console.WriteLine("Cadastro Realizado Com Sucesso");
            }
        }

        public static void UpdateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Id = 2,
                Bio = "Equipe | natan.io",
                Email = "hello@natan.io",
                Image = "https://...",
                Name = "Equipe de suporte natan.io",
                PasswordHash = "HASH",
                Slug = "equipe-natan"
            };
            using (connection)
            {
                connection.Update<User>(user);

                Console.WriteLine("Atualização Realizada Com Sucesso");
            }
        }

        public static void DeleteUser(SqlConnection connection)
        {

            using (connection)
            { 
                var user = connection.Get<User>(2);
                connection.Delete<User>(user);

                Console.WriteLine("Exclusão Realizada Com Sucesso");
            }
        }
    }
}
