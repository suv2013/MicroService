﻿using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder application)
        {
            using (var serviceScope = application.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                if (context != null)
                {
                    SeedData(context);
                }
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any())
            {
                Console.WriteLine("Seeding Data.");

                context.Platforms.AddRange(
                        new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                        new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                        new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                        );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("We already have data.");
            }
        }
    }
}
