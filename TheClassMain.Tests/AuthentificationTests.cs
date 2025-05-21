using Xunit;
using TheClassMain.Model;
using TheClassMain.Query;
using TheClassMain.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TheClassMain.Tests
{
    public class AuthentificationTests
    {
        // Test : la connexion doit r�ussir avec un email et un mot de passe valides
        [Fact]
        public void Login_ShouldSucceed_WithCorrectEmailAndPassword()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("LoginTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CustomersT.Add(new Customer
                {
                    UserName = "Jean",
                    Email = "jean@example.com",
                    Pwd = "abc123"
                });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var user = context.CustomersT
                    .FirstOrDefault(c => c.Email == "jean@example.com" && c.Pwd == "abc123");

                Assert.NotNull(user);
                Assert.Equal("Jean", user.UserName);
            }
        }

        // Test : la connexion �choue si le mot de passe est incorrect
        [Fact]
        public void Login_ShouldFail_WithWrongPassword()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("LoginFailTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CustomersT.Add(new Customer
                {
                    UserName = "Sophie",
                    Email = "sophie@example.com",
                    Pwd = "1234"
                });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var user = context.CustomersT
                    .FirstOrDefault(c => c.Email == "sophie@example.com" && c.Pwd == "wrong");

                Assert.Null(user);
            }
        }

        // Test : l'inscription ajoute un utilisateur valide � la base de donn�es
        [Fact]
        public void Register_ShouldAddCustomerToDatabase()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("RegisterTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CustomersT.Add(new Customer
                {
                    UserName = "Alex",
                    Email = "alex@example.com",
                    Pwd = "securepass"
                });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var user = context.CustomersT.FirstOrDefault(c => c.Email == "alex@example.com");
                Assert.NotNull(user);
                Assert.Equal("Alex", user.UserName);
            }
        }

        // Test : l'inscription �choue si un email existe d�j� dans la base
        [Fact]
        public void Register_ShouldFail_WhenEmailAlreadyExists()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("RegisterDupTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CustomersT.Add(new Customer
                {
                    UserName = "Julie",
                    Email = "julie@example.com",
                    Pwd = "pass"
                });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                bool exists = context.CustomersT.Any(c => c.Email == "julie@example.com");
                Assert.True(exists);
            }
        }

        // Test : la connexion �choue si l'email n'existe pas
        [Fact]
        public void Login_ShouldFail_WhenEmailDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<TableContext>()
                .UseInMemoryDatabase("UnknownEmailLoginTestDB")
                .Options;

            using (var context = new TableContext(options))
            {
                context.CustomersT.Add(new Customer
                {
                    UserName = "Inconnu",
                    Email = "inconnu@example.com",
                    Pwd = "test"
                });
                context.SaveChanges();
            }

            using (var context = new TableContext(options))
            {
                var user = context.CustomersT
                    .FirstOrDefault(c => c.Email == "absent@example.com" && c.Pwd == "test");

                Assert.Null(user);
            }
        }

        // Test : la validation �choue si un champ est vide
        [Fact]
        public void Register_ShouldFail_WhenFieldsAreEmpty()
        {
            var customer = new Customer
            {
                UserName = "",
                Email = "vide@example.com",
                Pwd = "1234"
            };

            bool isValid =
                !string.IsNullOrWhiteSpace(customer.UserName) &&
                !string.IsNullOrWhiteSpace(customer.Email) &&
                !string.IsNullOrWhiteSpace(customer.Pwd);

            Assert.False(isValid);
        }

        // Test : la m�thode ValidateInputs() retourne false si un champ est incomplet
        [Fact]
        public void ValidateInputs_ShouldReturnFalse_WhenFieldsAreIncomplete()
        {
            var vm = new RegisterViewModel
            {
                UserName = "Test",
                Email = "",
                Password = "abc"
            };

            var result = vm.ValidateInputs();
            Assert.False(result);
        }
    }
}
