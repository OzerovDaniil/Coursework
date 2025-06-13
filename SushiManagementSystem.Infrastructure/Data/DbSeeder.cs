using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            // Додаємо працівників
            if (!context.Employees.Any())
            {
                context.Employees.AddRange(
                    new Employee 
                    { 
                        Username = "admin", 
                        PasswordHash = "hashed123", 
                        Role = "Admin",
                        Email = "admin@sushi.com",
                        PhoneNumber = "+380501234567"
                    },
                    new Employee 
                    { 
                        Username = "chef1", 
                        PasswordHash = "hashed456", 
                        Role = "Chef",
                        Email = "chef1@sushi.com",
                        PhoneNumber = "+380502345678"
                    },
                    new Employee 
                    { 
                        Username = "cashier1", 
                        PasswordHash = "hashed789", 
                        Role = "Cashier",
                        Email = "cashier1@sushi.com",
                        PhoneNumber = "+380503456789"
                    }
                );
                context.SaveChanges();
            }

            // Додаємо клієнтів
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer 
                    { 
                        Name = "Олексій",
                        Surname = "Петренко",
                        Username = "alex",
                        PasswordHash = "pass123",
                        Email = "alex@email.com",
                        PhoneNumber = "+380504567890",
                        Address = "вул. Шевченка, 10, кв. 5"
                    },
                    new Customer 
                    { 
                        Name = "Марія",
                        Surname = "Коваленко",
                        Username = "maria",
                        PasswordHash = "pass456",
                        Email = "maria@email.com",
                        PhoneNumber = "+380505678901",
                        Address = "вул. Франка, 15, кв. 12"
                    }
                );
                context.SaveChanges();
            }

            // Додаємо інгредієнти та інвентар
            if (!context.Ingredients.Any())
            {
                var rice = new Ingredient
                {
                    Name = "Рис",
                    Description = "Японський рис для суші",
                    PricePerUnit = 50.00m,
                    Unit = "кг",
                    IsAvailable = true,
                    Category = "База",
                    Inventory = new Inventory
                    {
                        Quantity = 100,
                        LastUpdated = DateTime.UtcNow,
                        ItemName = "Рис",
                        Unit = "кг"
                    }
                };

                var salmon = new Ingredient
                {
                    Name = "Лосось",
                    Description = "Свіжий лосось для суші",
                    PricePerUnit = 800.00m,
                    Unit = "кг",
                    IsAvailable = true,
                    Category = "Риба",
                    Inventory = new Inventory
                    {
                        Quantity = 20,
                        LastUpdated = DateTime.UtcNow,
                        ItemName = "Лосось",
                        Unit = "кг"
                    }
                };

                context.Ingredients.Add(rice);
                context.Ingredients.Add(salmon);
                context.SaveChanges();
            }

            // Додаємо пункти меню
            if (!context.MenuItems.Any())
            {
                context.MenuItems.AddRange(
                    new MenuItem 
                    { 
                        Name = "Філадельфія",
                        Price = 350,
                        Description = "Класичний рол з лососем та сиром",
                        ImageUrl = "/images/philadelphia.jpg",
                        Category = "Роли",
                        IsAvailable = true
                    },
                    new MenuItem 
                    { 
                        Name = "Каліфорнія",
                        Price = 300,
                        Description = "Рол з крабом та авокадо",
                        ImageUrl = "/images/california.jpg",
                        Category = "Роли",
                        IsAvailable = true
                    }
                );
                context.SaveChanges();
            }

            // Додаємо рецепти
            if (!context.RecipeItems.Any())
            {
                var philadelphia = context.MenuItems.First(m => m.Name == "Філадельфія");
                var california = context.MenuItems.First(m => m.Name == "Каліфорнія");
                var rice = context.Ingredients.First(i => i.Name == "Рис");
                var salmon = context.Ingredients.First(i => i.Name == "Лосось");

                context.RecipeItems.Add(
                    new RecipeItem 
                    { 
                        MenuItemId = philadelphia.Id,
                        IngredientId = rice.Id,
                        Quantity = 100,
                        Unit = "г",
                        SpecialInstructions = "Рис має бути теплим",
                        MenuItem = philadelphia,
                        Ingredient = rice
                    }
                );
                context.SaveChanges();

                context.RecipeItems.Add(
                    new RecipeItem 
                    { 
                        MenuItemId = philadelphia.Id,
                        IngredientId = salmon.Id,
                        Quantity = 50,
                        Unit = "г",
                        SpecialInstructions = "Лосось має бути свіжим",
                        MenuItem = philadelphia,
                        Ingredient = salmon
                    }
                );
                context.SaveChanges();
            }

            // Додаємо замовлення
            if (!context.Orders.Any())
            {
                var customer = context.Customers.First(c => c.Username == "alex");
                var employee = context.Employees.First(e => e.Username == "cashier1");
                var philadelphia = context.MenuItems.First(m => m.Name == "Філадельфія");
                var california = context.MenuItems.First(m => m.Name == "Каліфорнія");

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.InProgress,
                    UserId = customer.Id,
                    TotalPrice = 650,
                    DeliveryAddress = customer.Address,
                    PaymentMethod = "Карта",
                    DeliveryDate = DateTime.Now.AddHours(1),
                    DeliveryStatus = DeliveryStatus.Pending,
                    Comment = "Додати соус уні",
                    CustomerId = customer.Id,
                    EmployeeId = employee.Id,
                    Customer = customer,
                    Employee = employee
                };

                context.Orders.Add(order);
                context.SaveChanges();

                context.OrderItems.Add(
                    new OrderItem 
                    { 
                        OrderId = order.Id,
                        MenuItemId = philadelphia.Id,
                        Quantity = 1,
                        Price = philadelphia.Price,
                        SpecialInstructions = "Без імбиру",
                        Order = order,
                        MenuItem = philadelphia
                    }
                );
                context.SaveChanges();

                context.OrderItems.Add(
                    new OrderItem 
                    { 
                        OrderId = order.Id,
                        MenuItemId = california.Id,
                        Quantity = 1,
                        Price = california.Price,
                        SpecialInstructions = "Додатково спайсі",
                        Order = order,
                        MenuItem = california
                    }
                );
                context.SaveChanges();
            }
        }
    }
} 