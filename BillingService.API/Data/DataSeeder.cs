using BillingService.API.Models;
using BillingService.API.Helpers;


namespace BillingService.API.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Seed Admin User
            if (!context.Users.Any(u => u.Email == "admin2@saas.com"))
            {
                context.Users.Add(new User
                {
                    FullName = "System Admin",
                    Email = "admin2@saas.com",
                    PasswordHash = PasswordHasher.HashPassword("admin123"),
                    Role = "Admin"
                });
            }



            //  Seed Subscription Plans
            if (!context.SubscriptionPlans.Any())
            {
                context.SubscriptionPlans.AddRange(
                    new SubscriptionPlan
                    {
                        Name = "Basic Plan",
                        Price = 199,
                        DurationInDays = 30
                    },
                    new SubscriptionPlan
                    {
                        Name = "Pro Plan",
                        Price = 499,
                        DurationInDays = 90
                    },
                    new SubscriptionPlan
                    {
                        Name = "Enterprise Plan",
                        Price = 999,
                        DurationInDays = 365
                    }
                );
            }

            context.SaveChanges();
        }
    }
}
