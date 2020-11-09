using BankAccount.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BankAccount.Data.Tests
{
    [TestClass()]
    public class InMemoryTests
    {
        [TestMethod()]
        public void InitializeInitializeShouldHaveGenerateData()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using (BankAccountContext context = new BankAccountContext(builder.Options))
            {
                DataGenerator.Seed(context);
                context.Database.EnsureCreated();
                var bank = context.Banks.FirstOrDefault();
                Assert.IsNotNull(bank, "The initializer should at least have created 1 Bank");
                var user = context.Users.FirstOrDefault();
                Assert.IsNotNull(bank, "The initializer should at least have created 1 User");
            }
        }
    }
}
