using Microsoft.EntityFrameworkCore;
using YungChingProject.Data;

namespace YungChingProject.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new NorthwindContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<NorthwindContext>>()))
            {
                // Look for any movies.
                if (context.Customers.Any())
                {
                    return;   // DB has been seeded
                }
                context.Customers.AddRange(
                    new Customer
                    {
                        CustomerId = "ALFKI",
                        CompanyName = "Alfreds Futterkiste",
                        ContactName = "Maria Anders",
                        ContactTitle = "Sales Representative",
                        Address = "Obere Str. 57",
                        City = "Berlin",
                        Area = null,
                        PostalCode = "12209",
                        Country = "Germany",
                        Phone = "030-0074321",
                        Fax = "030-0076545"
                    },
                    new Customer
                    {
                        CustomerId = "ANATR",
                        CompanyName = "Ana Trujillo Emparedados y helados",
                        ContactName = "Ana Trujillo",
                        ContactTitle = "Owner",
                        Address = "Avda. de la Constitucion 2222",
                        City = "Mexico D.F.",
                        Area = null,
                        PostalCode = "5021",
                        Country = "Mexico",
                        Phone = "(5) 555-4729",
                        Fax = "(5) 555-3745"
                    },
                    new Customer
                    {
                        CustomerId = "ANTON",
                        CompanyName = "Antonio Moreno Taqueria",
                        ContactName = "Antonio Moreno",
                        ContactTitle = "Owner",
                        Address = "Mataderos 2312",
                        City = "Mexico D.F.",
                        Area = null,
                        PostalCode = "5023",
                        Country = "Mexico",
                        Phone = "(5) 555-3932",
                        Fax = null
                    },
                    new Customer
                    {
                        CustomerId = "AROUT",
                        CompanyName = "Around the Horn",
                        ContactName = "Thomas Hardy",
                        ContactTitle = "Sales Representative",
                        Address = "120 Hanover Sq.",
                        City = "London",
                        Area = null,
                        PostalCode = "WA1 1DP",
                        Country = "UK",
                        Phone = "(171) 555-7788",
                        Fax = "(171) 555-6750"
                    },
                    new Customer
                    {
                        CustomerId = "BERGS",
                        CompanyName = "Berglunds snabbkop",
                        ContactName = "Christina Berglund",
                        ContactTitle = "Order Administrator",
                        Address = "Berguvsvagen 8",
                        City = "Lulea",
                        Area = null,
                        PostalCode = "S-958 22",
                        Country = "Sweden",
                        Phone = "0921-12 34 65",
                        Fax = "0921-12 34 67"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
