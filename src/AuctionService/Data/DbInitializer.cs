using System;
using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;


namespace AuctionService.Data;

public class DbInitializer
{
   public static void InitDb(WebApplication app)
   {
        using var scope = app.Services.CreateScope();

        SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());
   }

    private static void SeedData(AuctionDbContext context)
    {
        context.Database.Migrate();

        if(context.Auctions.Any())
        {
            Console.WriteLine("Already has data - no need to seed");
            return;
        }

        var auctions = new List<Auction>()
        {
            //
              new Auction
                {
                    Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Item = new Item
                    {
                        Make = "Rolls Royce",
                        Model = "Spectre",
                        Color = "White",
                        Mileage = 50000,
                        Year = 2020,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/06/01/08/30/rolls-royce-2362821_1280.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("5e116d89-66ea-4747-8cf5-30e23affa34d"), // Unique ID
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(10),
                    Item = new Item
                    {
                        Make = "Ford",
                        Model = "GT",
                        Color = "White",
                        Mileage = 50000,
                        Year = 2020,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("0ff4b6d8-da1a-4138-b54e-e446bf340356"), 
                    Status = Status.Live,
                    ReservePrice = 90000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(60),
                    Item = new Item
                    {
                        Make = "Bugatti",
                        Model = "Veyron",
                        Color = "Black",
                        Mileage = 15035,
                        Year = 2018,
                        ImageUrl = "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("a3b24c66-310b-4667-9e76-51f00ba584dd"),
                    Status = Status.Live,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(4),
                    Item = new Item
                    {
                        Make = "Ford",
                        Model = "Mustang",
                        Color = "Black",
                        Mileage = 65125,
                        Year = 2023,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/08/30/12/02/drive-1630501_1280.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("3c411770-b0c7-40a1-b6c3-8d5ce1b87a31"),
                    Status = Status.Live,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(4),
                    Item = new Item
                    {
                        Make = "Rolls Royce",
                        Model = "Gost",
                        Color = "Blue",
                        Mileage = 65125,
                        Year = 2023,
                        ImageUrl = "https://cdn.pixabay.com/photo/2014/09/05/13/34/car-436373_1280.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("c85725c0-a632-442c-8e67-a0ef078a84fd"), 
                    Status = Status.ReserveNotMet,
                    ReservePrice = 50000,
                    Seller = "tom",
                    AuctionEnd = DateTime.UtcNow.AddDays(-10),
                    Item = new Item
                    {
                        Make = "Mercedes",
                        Model = "SLK",
                        Color = "Silver",
                        Mileage = 15001,
                        Year = 2020,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/04/17/22/10/mercedes-benz-1335674_960_720.png"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("d3433109-f402-4b85-8205-317c83294c10"),
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(30),
                    Item = new Item
                    {
                        Make = "BMW",
                        Model = "X1",
                        Color = "White",
                        Mileage = 90000,
                        Year = 2017,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/08/31/05/47/bmw-2699538_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("628cd97e-ab8d-4177-8448-b9314dbcf953"), 
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(45),
                    Item = new Item
                    {
                        Make = "Ferrari",
                        Model = "Spider",
                        Color = "Red",
                        Mileage = 50000,
                        Year = 2015,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("00ace9df-3f9d-43ed-8ce5-1042ca3bc28f"), // Unique ID
                    Status = Status.Live,
                    ReservePrice = 150000,
                    Seller = "alice",
                    AuctionEnd = DateTime.UtcNow.AddDays(13),
                    Item = new Item
                    {
                        Make = "Ferrari",
                        Model = "F-430",
                        Color = "Red",
                        Mileage = 5000,
                        Year = 2022,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("cb38a57b-a7ee-43fc-9622-c5d2efbbbdda"), 
                    Status = Status.Live,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(19),
                    Item = new Item
                    {
                        Make = "Audi",
                        Model = "R8",
                        Color = "White",
                        Mileage = 10050,
                        Year = 2021,
                        ImageUrl = "https://cdn.pixabay.com/photo/2019/12/26/20/50/audi-r8-4721217_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("c1c47daa-e711-41dc-bdec-aa7538fc9875"), 
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "tom",
                    AuctionEnd = DateTime.UtcNow.AddDays(20),
                    Item = new Item
                    {
                        Make = "Audi",
                        Model = "TT",
                        Color = "Black",
                        Mileage = 25400,
                        Year = 2020,
                        ImageUrl = "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg"
                    }
                },
                new Auction
                {
                    Id = Guid.Parse("13f2dbe6-b570-425c-9551-e25d363233fe"), 
                    Status = Status.Live,
                    ReservePrice = 20000,
                    Seller = "bob",
                    AuctionEnd = DateTime.UtcNow.AddDays(48),
                    Item = new Item
                    {
                        Make = "Ford",
                        Model = "Model T",
                        Color = "Rust",
                        Mileage = 150150,
                        Year = 1938,
                        ImageUrl = "https://cdn.pixabay.com/photo/2017/08/02/19/47/vintage-2573090_960_720.jpg"
                    }
                },
            
            
            	    // 1 Ford GT
            new Auction
            {
                Id = Guid.Parse("f021c427-3008-40d6-b364-e260225d8ec2"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(10),
                Item = new Item
                {
                    Make = "Ford",
                    Model = "GT",
                    Color = "White",
                    Mileage = 50000,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"
                }
            },
            // 2 Bugatti Veyron
            new Auction
            {
                Id = Guid.Parse("0b1ca0e7-83fd-496d-bc8e-d3d979b0633a"),
                Status = Status.Live,
                ReservePrice = 90000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(60),
                Item = new Item
                {
                    Make = "Bugatti",
                    Model = "Veyron",
                    Color = "Black",
                    Mileage = 15035,
                    Year = 2018,
                    ImageUrl = "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg"
                }
            },
            // 3 Ford mustang
            new Auction
            {
                Id = Guid.Parse("94505bda-f0d4-4b58-badd-2b7524d0622f"),
                Status = Status.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(4),
                Item = new Item
                {
                    Make = "Ford",
                    Model = "Mustang",
                    Color = "Black",
                    Mileage = 65125,
                    Year = 2023,
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/09/06/17/15/mustang-927552_1280.jpg"
                }
            },
            // 4 Mercedes SLK
            new Auction
            {
                Id = Guid.Parse("53fa6d9f-cf4d-4644-9e16-6a2e5f9d8e30"),
                Status = Status.ReserveNotMet,
                ReservePrice = 50000,
                Seller = "tom",
                AuctionEnd = DateTime.UtcNow.AddDays(-10),
                Item = new Item
                {
                    Make = "Mercedes",
                    Model = "SLK",
                    Color = "Silver",
                    Mileage = 15001,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2020/01/31/17/34/mercedes-4808598_1280.jpg"
                }
            },
            // 5 BMW X1
            new Auction
            {
                Id = Guid.Parse("576945f0-c147-4b15-8b20-8b793fec17fa"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(30),
                Item = new Item
                {
                    Make = "BMW",
                    Model = "X1",
                    Color = "White",
                    Mileage = 90000,
                    Year = 2017,
                    ImageUrl = "https://cdn.pixabay.com/photo/2016/11/18/12/51/automobile-1834274_1280.jpg"
                }
            },
            // 6 Ferrari spider
            new Auction
            {
                Id = Guid.Parse("fabe75d4-53c0-4352-bb18-af5156bd5d02"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(45),
                Item = new Item
                {
                    Make = "Ferrari",
                    Model = "Spider",
                    Color = "Red",
                    Mileage = 50000,
                    Year = 2015,
                    ImageUrl = "https://cdn.pixabay.com/photo/2021/01/16/17/36/ferrari-5922873_1280.jpg"
                }
            },
            // 7 Ferrari F-430
            new Auction
            {
                Id = Guid.Parse("7b7e5102-7dc5-4570-96eb-365bd6663c25"),
                Status = Status.Live,
                ReservePrice = 150000,
                Seller = "alice",
                AuctionEnd = DateTime.UtcNow.AddDays(13),
                Item = new Item
                {
                    Make = "Ferrari",
                    Model = "F-430",
                    Color = "Red",
                    Mileage = 5000,
                    Year = 2022,
                    ImageUrl = "https://cdn.pixabay.com/photo/2018/01/18/18/00/ferrari-3090880_1280.jpg"
                }
            },
            // 8 Audi R8
            new Auction
            {
                Id = Guid.Parse("399c3a9c-887b-42c6-a54c-82bf1258b8e2"),
                Status = Status.Live,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(19),
                Item = new Item
                {
                    Make = "Audi",
                    Model = "R8",
                    Color = "White",
                    Mileage = 10050,
                    Year = 2021,
                    ImageUrl = "https://cdn.pixabay.com/photo/2021/11/01/21/20/car-6761801_1280.jpg"
                }
            },
            // 9 Audi TT
            new Auction
            {
                Id = Guid.Parse("d4d53031-81d1-4cca-a432-983aa8a06787"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "tom",
                AuctionEnd = DateTime.UtcNow.AddDays(20),
                Item = new Item
                {
                    Make = "Audi",
                    Model = "TT",
                    Color = "Black",
                    Mileage = 25400,
                    Year = 2020,
                    ImageUrl = "https://cdn.pixabay.com/photo/2015/01/19/13/51/car-604019_1280.jpg"
                }
            },
            // 10 Ford Model T
            new Auction
            {
                Id = Guid.Parse("b0054434-1ddf-4583-b4bb-422c8f50ded4"),
                Status = Status.Live,
                ReservePrice = 20000,
                Seller = "bob",
                AuctionEnd = DateTime.UtcNow.AddDays(48),
                Item = new Item
                {
                    Make = "Ford",
                    Model = "Model T",
                    Color = "Rust",
                    Mileage = 150150,
                    Year = 1938,
                    ImageUrl = "https://cdn.pixabay.com/photo/2018/10/27/23/19/ford-3777615_1280.jpg"
                }
            }
        };

        context.AddRange(auctions);
        context.SaveChanges();

    }
}
