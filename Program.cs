using System;
using Blog.Models;
using Blog.Repositories;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = @"Server=localhost,1433;Database=Blog;User ID=sa;Password=1q2w3e4r@#$";

        static void Main(string[] args)
        {
            using var connection = new SqlConnection(CONNECTION_STRING);
            var repository = new Repository<User>(connection);

            // CreateUser(repository);
            // ReadUser(repository);
            // ReadUsers(repository);
            // UpdateUser(repository);
            // DeleteUser(repository);
             ReadWithRoles(connection);
        }

        private static void CreateUser(Repository<User> repository)
        {
            var user = new User
            {
                Bio = "Estudando .Net",
                Email = "bruno.loures@hotmail.com",
                Image = "",
                Name = "Bruno Loures",
                Slug = "louresb",
                PasswordHash = Guid.NewGuid().ToString()
            };

            repository.Create(user);
        }

        private static void ReadUsers(Repository<User> repository)
        {
            var users = repository.Read();
            foreach (var item in users)
                Console.WriteLine(item.Name);
        }

        private static void ReadUser(Repository<User> repository)
        {
            var user = repository.Read(1);
            Console.WriteLine(user?.Email);
        }

        private static void UpdateUser(Repository<User> repository)
        {
            var user = repository.Read(1);
            user.Email = "brunoloures93@gmail.com";
            repository.Update(user);

            Console.WriteLine(user?.Email);
        }

        private static void DeleteUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            repository.Delete(user);
        }

        private static void ReadWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.ReadWithRole();

            foreach (var user in users)
            {
                Console.WriteLine(user.Email);
                foreach (var role in user.Roles) Console.WriteLine($" - {role.Slug}");
            }
        }
    }
}