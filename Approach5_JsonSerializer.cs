using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using MessagePack;
using MessagePack.Resolvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using ConsoleApp1;

namespace MessagePackSerializationDemo
{
    public class Approach5
    {
        public static SerializationResult RunApproach5()
        {
            // Create sample data
            var user = new CleanUser5
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile5
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address5
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order5>
                {
                    new Order5 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem5>(), TotalAmount = 345353 },
                    new Order5 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem5>(), TotalAmount = 345353 }
                },
                Address = new Address5
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
                SocialLinks = new List<SocialMedia5>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia5
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia5
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia5
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            // Measure serialization and deserialization time
            Stopwatch sw = new Stopwatch();

            sw.Start();
            string serializedData = JsonSerializer.Serialize(user);
            sw.Stop();
            long serializationTime = sw.ElapsedMilliseconds;

            sw.Restart();
            var deserializedObject = JsonSerializer.Deserialize<CleanUser5>(serializedData);
            sw.Stop();
            long deserializationTime = sw.ElapsedMilliseconds;

            // Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");
            Console.WriteLine($"Deserialized User Name: {deserializedObject.Name}");

            SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            return result;
        }
    }

    //public class CleanUser5
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public Profile5 Profile { get; set; }

    //    public List<Order5> Orders { get; set; }

    //    [JsonIgnore] // Ignored during JSON serialization
    //    public string Secret { get { return "Hello"; } }
    //}

    //public class Profile5
    //{
    //    public string Email { get; set; }

    //    public string Phone { get; set; }

    //    private string Secret = "hello";
    //}

    //public class Order5
    //{
    //    public int OrderId { get; set; }

    //    public DateTime OrderDate { get; set; }
    //}

    public class CleanUser5
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Profile5 Profile { get; set; }

        public List<Order5> Orders { get; set; }

        public Address5 Address { get; set; }

        public Dictionary<string, string> Preferences { get; set; }

        public List<int> FavoriteNumbers { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<SocialMedia5> SocialLinks { get; set; }

        [JsonIgnore] // Ignored during JSON serialization
        public string Secret { get { return "This is private"; } }
    }

    public class Profile5
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public Address5 PrimaryAddress { get; set; }

        private string InternalNote = "Internal Data";
    }

    public class Order5
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItem5> Items { get; set; }
    }

    public class OrderItem5
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }

    public class Address5
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }
    }

    public class SocialMedia5
    {
        public string Platform { get; set; }

        public string Url { get; set; }

        public string Username { get; set; }
    }
}