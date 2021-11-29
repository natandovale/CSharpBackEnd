using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using static System.Console;
using System.Data.SqlClient;
using Blog.Screens.TagScreens;

namespace Blog
{
    class Program
    {
        private const string SQLCONNECTION = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";

        public static void Main(string[] args)
        {
            Database.Connection = new SqlConnection(SQLCONNECTION);
            //var repository = new Repository<User>(connection);
            //var users = repository.Get();
            //foreach (var item in users)
            //{
            //    Console.WriteLine(item.Name);
            //}
            Database.Connection.Open();
            Load();
            ReadKey();
            Database.Connection.Close();
        }

        private static void Load()
        {
            Clear();
            WriteLine("Meu Blog");
            WriteLine("--------------");
            WriteLine("O que deseja fazer?");
            WriteLine();
            WriteLine("1 - Gestão de usuário");
            WriteLine("2 - Gestão de perfil");
            WriteLine("3 - Gestão de categoria");
            WriteLine("4 - Gestão de tag");
            WriteLine("5 - Vincular perfil/usuário");
            WriteLine("6 - Vincular post/tag");
            WriteLine("7 - Relatórios");
            WriteLine();
            WriteLine(); 
            var option = short.Parse(ReadLine());

            switch (option)
            {
                //case 1:
                //    MenuUserScreen.Load();
                //    break;
                //case 2:
                //    UpdateTagScreen.Load();
                //    break;
                //case 3:
                //    CreateTagScreen.Load();
                //    break;
                case 4:
                    MenuTagScreen.Load();
                    break;
                default:
                    break;
            }

        }
    }
}
