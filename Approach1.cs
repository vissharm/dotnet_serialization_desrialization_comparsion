using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using ConsoleApp1;
using MessagePack;
using Newtonsoft.Json;

namespace MessagePackSerializationDemo
{
    public class Approach1
    {
        public static SerializationResult RunApproach1(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach1_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order>
                {
                    new Order { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem>() { }, TotalAmount = 345353 },
                    new Order { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem>() { }, TotalAmount = 345353 }
                },
                Address = new Address
                {
                    City = "dummy",
                    Country = "india",
                    State = "punjab",
                    Street = "hell",
                    ZipCode = "000898"
                },
                CreatedAt = DateTime.UtcNow,
                FavoriteNumbers = new List<int> { 1, 2, 3000, 5000, 20 },
                Preferences = new Dictionary<string, string>() { },
                SocialLinks = new List<SocialMedia>() { }
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            // Serialization to file
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            using (var file = File.Create(filePath))
            {
                MessagePack.MessagePackSerializer.Serialize(file, user);
            }
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            using (var file = File.OpenRead(filePath))
            {
                var deserializedUser = MessagePack.MessagePackSerializer.Deserialize<CleanUser>(file);
                Console.WriteLine($"Deserialized User Name: {deserializedUser.Name}");
            }
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            //// Measure serialization and deserialization time
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //byte[] serializedData = MessagePackSerializer.Serialize(user);
            //sw.Stop();
            //long serializationTime = sw.ElapsedMilliseconds;

            //sw.Restart();
            //var deserializedUser = MessagePackSerializer.Deserialize<CleanUser>(serializedData);
            //sw.Stop();
            //long deserializationTime = sw.ElapsedMilliseconds;

            // Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");

            //SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            //return result;
            return new SerializationResult(serializationTime, deserializationTime);
        }
    }

    //[MessagePackObject]
    //[DataContract]
    //public class CleanUser1
    //{
    //    [Key(0)]
    //    [DataMember(Order = 1)]
    //    public int Id { get; set; }

    //    [Key(1)]
    //    [DataMember(Order = 2)]
    //    public string Name { get; set; }

    //    [Key(2)]
    //    public Profile1 Profile { get; set; }

    //    [Key(3)]
    //    [DataMember(Order = 3)]
    //    public List<Order1> Orders { get; set; }

    //    [IgnoreMember]
    //    [IgnoreDataMember]
    //    [JsonIgnore] // Ignored during JSON serialization
    //    public string Secret { get { return "Hello"; } }
    //}

    //[MessagePackObject]
    //[Serializable]
    //public class Profile1
    //{
    //    [Key(0)]
    //    public string Email { get; set; }

    //    [Key(1)]
    //    public string Phone { get; set; }

    //    [IgnoreMember]
    //    [NonSerialized] // Ignored during binary serialization
    //    private string Secret = "hello";
    //}

    //[MessagePackObject]
    //[Serializable]
    //public class Order1
    //{
    //    [Key(0)]
    //    [DataMember]
    //    public int OrderId { get; set; }

    //    [Key(1)]
    //    [DataMember]
    //    public DateTime OrderDate { get; set; }
    //}

    [MessagePackObject]
    [DataContract]
    public class CleanUser
    {
        [Key(0)]
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [Key(1)]
        [DataMember(Order = 2)]
        public string Name { get; set; }

        [Key(2)]
        [DataMember(Order = 3)]
        public Profile Profile { get; set; }

        [Key(3)]
        [DataMember(Order = 4)]
        public List<Order> Orders { get; set; }

        [Key(4)]
        [DataMember(Order = 5)]
        public Address Address { get; set; }

        [Key(5)]
        [DataMember(Order = 6)]
        public Dictionary<string, string> Preferences { get; set; }

        [Key(6)]
        [DataMember(Order = 7)]
        public List<int> FavoriteNumbers { get; set; }

        [Key(7)]
        [DataMember(Order = 8)]
        public DateTime CreatedAt { get; set; }

        [Key(8)]
        [DataMember(Order = 9)]
        public List<SocialMedia> SocialLinks { get; set; }

        [IgnoreMember]
        [IgnoreDataMember]
        [JsonIgnore] // Ignored during JSON serialization
        public string Secret { get { return "This is private"; } }
    }

    [MessagePackObject]
    [DataContract]
    public class Profile
    {
        [Key(0)]
        [DataMember(Order = 1)]
        public string Email { get; set; }

        [Key(1)]
        [DataMember(Order = 2)]
        public string Phone { get; set; }

        [Key(2)]
        [DataMember(Order = 3)]
        public DateTime BirthDate { get; set; }

        [Key(3)]
        [DataMember(Order = 4)]
        public Address PrimaryAddress { get; set; }

        [IgnoreMember]
        [NonSerialized] // Ignored during binary serialization
        private string InternalNote = "Internal Data";
    }

    [MessagePackObject]
    [DataContract]
    public class Order
    {
        [Key(0)]
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [Key(1)]
        [DataMember(Order = 2)]
        public DateTime OrderDate { get; set; }

        [Key(2)]
        [DataMember(Order = 3)]
        public decimal TotalAmount { get; set; }

        [Key(3)]
        [DataMember(Order = 4)]
        public List<OrderItem> Items { get; set; }
    }

    [MessagePackObject]
    [DataContract]
    public class OrderItem
    {
        [Key(0)]
        [DataMember(Order = 1)]
        public int ProductId { get; set; }

        [Key(1)]
        [DataMember(Order = 2)]
        public string ProductName { get; set; }

        [Key(2)]
        [DataMember(Order = 3)]
        public int Quantity { get; set; }

        [Key(3)]
        [DataMember(Order = 4)]
        public decimal Price { get; set; }
    }

    [MessagePackObject]
    [DataContract]
    public class Address
    {
        [Key(0)]
        [DataMember(Order = 1)]
        public string Street { get; set; }

        [Key(1)]
        [DataMember(Order = 2)]
        public string City { get; set; }

        [Key(2)]
        [DataMember(Order = 3)]
        public string State { get; set; }

        [Key(3)]
        [DataMember(Order = 4)]
        public string ZipCode { get; set; }

        [Key(4)]
        [DataMember(Order = 5)]
        public string Country { get; set; }
    }

    [MessagePackObject]
    [DataContract]
    public class SocialMedia
    {
        [Key(0)]
        [DataMember(Order = 1)]
        public string Platform { get; set; }

        [Key(1)]
        [DataMember(Order = 2)]
        public string Url { get; set; }

        [Key(2)]
        [DataMember(Order = 3)]
        public string Username { get; set; }
    }
}