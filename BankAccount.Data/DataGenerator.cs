using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankAccount.Data
{
    public class DataGenerator
    {

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (BankAccountContext context = new BankAccountContext(
                serviceProvider.GetRequiredService<DbContextOptions<BankAccountContext>>()))
            {
                Seed(context);
            }
        }
        public static void Seed(BankAccountContext context, string userName = "Partik", string bankName = "The Awsome Bank")
        {
            if (context.Banks.Any())
            {
                return;
            }
            Domain.Bank newBank = new Domain.Bank()
            {
                BankName = bankName
            };
            context.Banks.Add(newBank);
            Domain.User newUser = new Domain.User()
            {
                Name = userName,
                Bank = newBank
            };
            context.Users.Add(newUser);
            context.SaveChanges();
        }
    }
}

