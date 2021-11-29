using Dapper;
using IntroducaoDapper.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace IntroducaoDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            const string connectionString = "Server=localhost,1433;Database=balta;User ID=sa;Password=1q2w3e4r@#$";

            using (var connection = new SqlConnection(connectionString))
            {
                //CreateCategory(connection);
                //CreateManyCategory(connection);
                //UpdateCategory(connection);
                //ExecuteProcedure(connection);
                //ExecuteReadProcedure(connection);
                //ListCategories(connection);
                //OneToOne(connection);
                //OneToMany(connection);
                //QueryMultiple(connection);
                //SelectIn(connection);
                //Like(connection, "api");
                //Transaction(connection);
            }
        }

        static void ListCategories(SqlConnection connection)
        {
            var itens = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in itens)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }
        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviços do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO [Category] 
                            VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured,
            });
            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = "UPDATE [Category] SET [Title] = @title WHERE [Id] = @id";
            var rows = connection.Execute(updateQuery, new
            {
                id = "af3407aa-11ae-4621-a2ef-2028b85507c4",
                title = "Frontend 2021"
            });
            Console.WriteLine($"{rows} registros atualizadas");
        }

        static void CreateManyCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Azure Microsoft";
            category.Url = "azure";
            category.Description = "Categoria destinada a serviços da Microsoft";
            category.Order = 9;
            category.Summary = "Azure Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria nova";
            category2.Order = 10;
            category2.Summary = "Categoria";
            category2.Featured = true;

            var insertSql = @"INSERT INTO [Category] 
                            VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

            var rows = connection.Execute(insertSql, new[]
            {
                new {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured,
            },
                new {
                category2.Id,
                category2.Title,
                category2.Url,
                category2.Summary,
                category2.Order,
                category2.Description,
                category2.Featured,
            }});
            Console.WriteLine($"{rows} linhas inseridas");
        }

        static void ExecuteProcedure(SqlConnection connection)
        {
            var procedure = "[spDeleteStudent]";
            var parms = new { StudentId = "088c7314-c192-4134-ace1-37f4f89c9d19" };
            var rows = connection.Execute(procedure, parms, commandType: CommandType.StoredProcedure);
            Console.WriteLine(rows);
        }

        static void ExecuteReadProcedure(SqlConnection connection)
        {
            var procedure = "[spGetCourseByCategory]";
            var parms = new { CategoryId = "af3407aa-11ae-4621-a2ef-2028b85507c4" };
            var courses = connection.Query(procedure, parms, commandType: CommandType.StoredProcedure);
            foreach (var item in courses)
            {
                Console.WriteLine(item.Id);
            }
        }

        static void OneToOne(SqlConnection connection)
        {
            var sql = @"SELECT * FROM [CareerItem]
                        INNER JOIN [Course] ON [CareerItem].[CourseId] = [Course].[Id]";
            var items = connection.Query<CareerItem, Course, CareerItem>(sql,
                (careerItem, course) =>
                {
                    careerItem.Course = course;
                    return careerItem;
                }, splitOn: "Id");

            foreach (var item in items)
            {
                Console.WriteLine($"Carreira: {item.Title} - Curso: {item.Course.Title}");
            }
        }

        static void OneToMany(SqlConnection connection)
        {
            var sql = @"SELECT [Career].[Id],
                               [Career].[Title],
                               [CareerItem].[CareerId],
                               [CareerItem].[Title] 
                        FROM [Career] 
                        INNER JOIN 
                               [CareerItem] ON [CareerItem].[CareerId] = [Career].[Id]
                        ORDER BY 
                               [Career].[Title]";

            var careers = new List<Career>();
            var items = connection.Query<Career, CareerItem, Career>(sql,
                (career, item) =>
                {
                    //Verificando se o item existe na carreira(career)
                    var car = careers.Where(x => x.Id == career.Id).FirstOrDefault();
                    
                    if(car == null)
                    {
                        car = career;
                        car.Items.Add(item);
                        careers.Add(car);
                    }
                    else
                    {
                        car.Items.Add(item);
                    }
                    return career;
                }, splitOn: "CareerId");

            foreach (var career in careers)
            {
                Console.WriteLine($"Carreira: {career.Title} ");
                foreach (var item in career.Items)
                {
                    Console.WriteLine($"--{item.Title} ");
                }
            }
        }

        //"ManyToMany"
        static void QueryMultiple(SqlConnection connection)
        {
            var query = "SELECT * FROM [Category]; SELECT * FROM [Course]";
            using(var multi = connection.QueryMultiple(query))
            {
                var categories = multi.Read<Category>();
                var courses = multi.Read<Course>();

                foreach (var item in categories)
                {
                    Console.WriteLine(item.Title);
                }

                foreach (var item in courses)
                {
                    Console.WriteLine(item.Title);
                }
            }
        }

        static void SelectIn(SqlConnection connection)
        {
            var query = @"SELECT * FROM Career WHERE [Id] IN @Id";
            var items = connection.Query<Career>(query, new
            {
                Id = new[]
                {
                    "01ae8a85-b4e8-4194-a0f1-1c6190af54cb",
                    "e6730d1c-6870-4df3-ae68-438624e04c72"
                }
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Like(SqlConnection connection, string term)
        {
            var query = @"SELECT * FROM [Course] WHERE [Title] LIKE @exp";
            var items = connection.Query<Career>(query, new
            {
                exp = $"%{term}%"
            });

            foreach (var item in items)
            {
                Console.WriteLine(item.Title);
            }
        }

        static void Transaction(SqlConnection connection)
        {

            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Azure";
            category.Url = "azure";
            category.Description = "Categoria que n quero";
            category.Order = 9;
            category.Summary = "Azure Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO [Category] 
                            VALUES(@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                var rows = connection.Execute(insertSql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured,
                }, transaction);
                transaction.Commit();
                //transaction.Rollback();
                Console.WriteLine($"{rows} linhas inseridas");
            }
        }
    }
}
