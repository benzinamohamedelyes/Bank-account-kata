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
                // Look for any board games.
                if (context.Banks.Any())
                {
                    return;   // Data was already seeded
                }
            }
        }
    }
}

