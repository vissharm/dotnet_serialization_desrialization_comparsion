using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleApp1;
using MessagePack;

namespace MessagePackSerializationDemo
{
    public class Approach2
    {
        public static SerializationResult RunApproach2()
        {
            // Create sample data
            var user = new CleanUser2
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile2
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address2
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order2>
                {
                    new Order2 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem2>(), TotalAmount = 345353 },
                    new Order2 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem2>(), TotalAmount = 345353 }
                },
                Address = new Address2
                {
                    City = "dummy",
                    Country = "india",
                    State = "punjab",
                    Street = "hell",
                    ZipCode = "000898"
                },
                CreatedAt = DateTime.UtcNow,
                FavoriteNumbers = new List<int> { 1, 2, 3000, 5000, 20 },
                Preferences = new Dictionary<string, string>(),
                SocialLinks = new List<SocialMedia2>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia2
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia2
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia2
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            // Measure serialization and deserialization time
            Stopwatch sw = new Stopwatch();

            sw.Start();
            byte[] serializedData = MessagePackSerializer.Serialize(user);
            sw.Stop();
            long serializationTime = sw.ElapsedMilliseconds;

            sw.Restart();
            var deserializedUser = MessagePackSerializer.Deserialize<CleanUser2>(serializedData);
            sw.Stop();
            long deserializationTime = sw.ElapsedMilliseconds;

            // Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");
            Console.WriteLine($"Deserialized User Name: {deserializedUser.Name}");

            SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            return result;
        }
    }

    //[MessagePackObject]
    //public class CleanUser2
    //{
    //    [Key(0)]
    //    public int Id { get; set; }

    //    [Key(1)]
    //    public string Name { get; set; }

    //    [Key(2)]
    //    public Profile2 Profile { get; set; }

    //    [Key(3)]
    //    public List<Order2> Orders { get; set; }

    //    [IgnoreMember]
    //    public string Secret { get { return "Hello"; } }
    //}

    //[MessagePackObject]
    //public class Profile2
    //{
    //    [Key(0)]
    //    public string Email { get; set; }

    //    [Key(1)]
    //    public string Phone { get; set; }

    //    [IgnoreMember]
    //    private string Secret = "hello";
    //}

    //[MessagePackObject]
    //public class Order2
    //{
    //    [Key(0)]
    //    public int OrderId { get; set; }

    //    [Key(1)]
    //    public DateTime OrderDate { get; set; }
    //}


    [MessagePackObject]
    public class CleanUser2
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Name { get; set; }

        [Key(2)]
        public Profile2 Profile { get; set; }

        [Key(3)]
        public List<Order2> Orders { get; set; }

        [Key(4)]
        public Address2 Address { get; set; }

        [Key(5)]
        public Dictionary<string, string> Preferences { get; set; }

        [Key(6)]
        public List<int> FavoriteNumbers { get; set; }

        [Key(7)]
        public DateTime CreatedAt { get; set; }

        [Key(8)]
        public List<SocialMedia2> SocialLinks { get; set; }

        [IgnoreMember]
        public string Secret { get { return "This is private"; } }
    }

    [MessagePackObject]
    public class Profile2
    {
        [Key(0)]
        public string Email { get; set; }

        [Key(1)]
        public string Phone { get; set; }

        [Key(2)]
        public DateTime BirthDate { get; set; }

        [Key(3)]
        public Address2 PrimaryAddress { get; set; }

        [IgnoreMember]
        private string InternalNote = "Internal Data";
    }

    [MessagePackObject]
    public class Order2
    {
        [Key(0)]
        public int OrderId { get; set; }

        [Key(1)]
        public DateTime OrderDate { get; set; }

        [Key(2)]
        public decimal TotalAmount { get; set; }

        [Key(3)]
        public List<OrderItem2> Items { get; set; }
    }

    [MessagePackObject]
    public class OrderItem2
    {
        [Key(0)]
        public int ProductId { get; set; }

        [Key(1)]
        public string ProductName { get; set; }

        [Key(2)]
        public int Quantity { get; set; }

        [Key(3)]
        public decimal Price { get; set; }
    }

    [MessagePackObject]
    public class Address2
    {
        [Key(0)]
        public string Street { get; set; }

        [Key(1)]
        public string City { get; set; }

        [Key(2)]
        public string State { get; set; }

        [Key(3)]
        public string ZipCode { get; set; }

        [Key(4)]
        public string Country { get; set; }
    }

    [MessagePackObject]
    public class SocialMedia2
    {
        [Key(0)]
        public string Platform { get; set; }

        [Key(1)]
        public string Url { get; set; }

        [Key(2)]
        public string Username { get; set; }
    }
}