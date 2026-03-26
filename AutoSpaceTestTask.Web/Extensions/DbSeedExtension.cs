using AutoSpaceTestTask.Database.Context;
using AutoSpaceTestTask.Database.Entities;

namespace AutoSpaceTestTask.Web.Extensions
{
    public static class DbSeedExtension
    {
        public static WebApplication SeedDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            ClearAllTables(context);

            Seed(context);

            return app;
        }

        private static void ClearAllTables(AppDbContext context)
        {
            var storeProducts = context.StoreProducts.ToList();
            context.StoreProducts.RemoveRange(storeProducts);

            var storeSchedules = context.StoreSchedules.ToList();
            context.StoreSchedules.RemoveRange(storeSchedules);

            var products = context.Products.ToList();
            context.Products.RemoveRange(products);

            var stores = context.Stores.ToList();
            context.Stores.RemoveRange(stores);

            var productGroups = context.ProductGroups.ToList();
            context.ProductGroups.RemoveRange(productGroups);

            context.SaveChanges();
        }


        private static void Seed(AppDbContext context)
        {
            var engineParts = new ProductGroup { Name = "Engine Parts" };
            var suspensionParts = new ProductGroup { Name = "Suspension Parts" };
            var electricalParts = new ProductGroup { Name = "Electrical Parts" };

            context.ProductGroups.AddRange(engineParts, suspensionParts, electricalParts);
            context.SaveChanges();

            var products = new List<Product>
            {
                new() { Code = Guid.NewGuid(), Article = "ENG-001", Brand = "Bosch", Name = "Oil Filter", GroupId = engineParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ENG-002", Brand = "Mahle", Name = "Air Filter", GroupId = engineParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ENG-003", Brand = "Denso", Name = "Spark Plug", GroupId = engineParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ENG-004", Brand = "Valeo", Name = "Fuel Pump", GroupId = engineParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ENG-005", Brand = "Gates", Name = "Timing Belt", GroupId = engineParts.Id },

                new() { Code = Guid.NewGuid(), Article = "SUS-001", Brand = "Monroe", Name = "Shock Absorber", GroupId = suspensionParts.Id },
                new() { Code = Guid.NewGuid(), Article = "SUS-002", Brand = "KYB", Name = "Strut Mount", GroupId = suspensionParts.Id },
                new() { Code = Guid.NewGuid(), Article = "SUS-003", Brand = "Lemforder", Name = "Control Arm", GroupId = suspensionParts.Id },
                new() { Code = Guid.NewGuid(), Article = "SUS-004", Brand = "Febi", Name = "Stabilizer Link", GroupId = suspensionParts.Id },
                new() { Code = Guid.NewGuid(), Article = "SUS-005", Brand = "TRW", Name = "Ball Joint", GroupId = suspensionParts.Id },

                new() { Code = Guid.NewGuid(), Article = "ELE-001", Brand = "Bosch", Name = "Car Battery", GroupId = electricalParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ELE-002", Brand = "Hella", Name = "Headlight Bulb", GroupId = electricalParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ELE-003", Brand = "Valeo", Name = "Alternator", GroupId = electricalParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ELE-004", Brand = "Denso", Name = "Starter Motor", GroupId = electricalParts.Id },
                new() { Code = Guid.NewGuid(), Article = "ELE-005", Brand = "NGK", Name = "Ignition Coil", GroupId = electricalParts.Id },
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            var stores = new List<Store>
            {
                new() { Code = Guid.NewGuid(), Name = "AutoParts Central", Address = "12 Main Street" },
                new() { Code = Guid.NewGuid(), Name = "DriveTech Store", Address = "45 Industrial Ave" },
                new() { Code = Guid.NewGuid(), Name = "CarFix Supplies", Address = "78 Market Road" },
                new() { Code = Guid.NewGuid(), Name = "MotorHub", Address = "23 Garage Lane" },
                new() { Code = Guid.NewGuid(), Name = "Premium Auto Parts", Address = "90 Service Blvd" },
            };

            context.Stores.AddRange(stores);
            context.SaveChanges();

            var rnd = new Random();

            var storeProducts = new List<StoreProduct>();

            foreach (var store in stores)
            {
                var assignedProducts = products
                    .OrderBy(_ => rnd.Next())
                    .Take(7)
                    .ToList();

                storeProducts.AddRange(
                    assignedProducts.Select(p => new StoreProduct
                    {
                        StoreId = store.Id,
                        ProductId = p.Id
                    }));
            }

            context.StoreProducts.AddRange(storeProducts);

            var schedules = new List<StoreSchedule>();

            for (int storeIndex = 0; storeIndex < stores.Count; storeIndex++)
            {
                var store = stores[storeIndex];

                var dayOff = (DayOfWeek)(storeIndex % 7);

                for (int i = 0; i < 7; i++)
                {
                    var day = (DayOfWeek)i;

                    schedules.Add(new StoreSchedule
                    {
                        StoreId = store.Id,
                        DayOfWeek = day,
                        IsDayOff = day == dayOff,
                        OpenTime = new TimeOnly(9, 0),
                        CloseTime = new TimeOnly(18, 0)
                    });
                }
            }

            context.StoreSchedules.AddRange(schedules);

            context.SaveChanges();
        }
    }
}