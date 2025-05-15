using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using MessagePack;
using MessagePack.Resolvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using ConsoleApp1;
using Jil;
using DeepEqual.Syntax;
using Ceras;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach9
    {
        public static void RunApproach9(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach9_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser9
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile9
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address9
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order9>
                {
                    new Order9 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem9>(), TotalAmount = 345353 },
                    new Order9 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem9>(), TotalAmount = 345353 }
                },
                Address = new Address9
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
                SocialLinks = new List<SocialMedia9>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia9
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia9
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia9
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser9>(user, 500000);
            Console.WriteLine("Done creating 10 lakh objects list");

            // Serialization to file
            // No circular reference support
            // only public properties not fields
            var stopwatch = Stopwatch.StartNew();
            var options = new Options(
                 prettyPrint: false,
                 excludeNulls: false,
                 includeInherited: true,
                 serializationNameFormat: SerializationNameFormat.CamelCase,
                 dateFormat: Jil.DateTimeFormat.ISO8601
             );
            using (var writer = new StreamWriter(filePath))
            {
                JSON.Serialize(userlist, writer);
            }
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            List<CleanUser9> deserializedUser;
            using (var reader = new StreamReader(filePath))
            {
                deserializedUser = JSON.Deserialize<List<CleanUser9>>(reader);
            }
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            // Output the deserialized data
            Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name}");

            // Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");

            // Compare the original and deserialized lists
            //bool areListsEqual = DeepEqualityChecker.AreListsDeeplyEqual(userlist, deserializedUser);
            //Console.WriteLine($"Are the original and deserialized lists deeply equal? {areListsEqual}");
            //return new SerializationResult(serializationTime, deserializationTime);
            bool areEqual = userlist.IsDeepEqual(deserializedUser);
            Console.WriteLine($"Are the lists deeply equal? {areEqual}");
        }
    }

    //[DataContract]
    //public class CleanUser6
    //{
    //    [DataMember(Order = 1)]
    //    public int Id { get; set; }

    //    [DataMember(Order = 2)]
    //    public string Name { get; set; }

    //    public Profile6 Profile { get; set; }

    //    [DataMember(Order = 3)]
    //    public List<Order6> Orders { get; set; }

    //    [JsonIgnore] // Ignored during JSON serialization
    //    [IgnoreDataMember]
    //    public string Secret { get { return "Hello"; } }
    //}

    //[DataContract]
    //public class Profile6
    //{
    //    [DataMember(Order = 1)]
    //    public string Email { get; set; }

    //    [DataMember(Order = 2)]
    //    public string Phone { get; set; }

    //    [IgnoreDataMember]
    //    private string Secret = "hello";
    //}

    //[DataContract]
    //public class Order6
    //{
    //    [DataMember]
    //    public int OrderId { get; set; }

    //    [DataMember]
    //    public DateTime OrderDate { get; set; }
    //}

    [DataContract]
    public class CleanUser9
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public Profile9 Profile { get; set; }

        [DataMember(Order = 4)]
        public List<Order9> Orders { get; set; }

        [DataMember(Order = 5)]
        public Address9 Address { get; set; }

        [DataMember(Order = 6)]
        public Dictionary<string, string> Preferences { get; set; }

        [DataMember(Order = 7)]
        public List<int> FavoriteNumbers { get; set; }

        [DataMember(Order = 8)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Order = 9)]
        public List<SocialMedia9> SocialLinks { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public string Secret { get { return "This is private"; } }
    }

    [DataContract]
    public class Profile9
    {
        [DataMember(Order = 1)]
        public string Email { get; set; }

        [DataMember(Order = 2)]
        public string Phone { get; set; }

        [DataMember(Order = 3)]
        public DateTime BirthDate { get; set; }

        [DataMember(Order = 4)]
        public Address9 PrimaryAddress { get; set; }

        [IgnoreDataMember]
        private string InternalNote = "Internal Data";
    }

    [DataContract]
    public class Order9
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [DataMember(Order = 2)]
        public DateTime OrderDate { get; set; }

        [DataMember(Order = 3)]
        public decimal TotalAmount { get; set; }

        [DataMember(Order = 4)]
        public List<OrderItem9> Items { get; set; }
    }

    [DataContract]
    public class OrderItem9
    {
        [DataMember(Order = 1)]
        public int ProductId { get; set; }

        [DataMember(Order = 2)]
        public string ProductName { get; set; }

        [DataMember(Order = 3)]
        public int Quantity { get; set; }

        [DataMember(Order = 4)]
        public decimal Price { get; set; }
    }

    [DataContract]
    public class Address9
    {
        [DataMember(Order = 1)]
        public string Street { get; set; }

        [DataMember(Order = 2)]
        public string City { get; set; }

        [DataMember(Order = 3)]
        public string State { get; set; }

        [DataMember(Order = 4)]
        public string ZipCode { get; set; }

        [DataMember(Order = 5)]
        public string Country { get; set; }
    }

    [DataContract]
    public class SocialMedia9
    {
        [DataMember(Order = 1)]
        public string Platform { get; set; }

        [DataMember(Order = 2)]
        public string Url { get; set; }

        [DataMember(Order = 3)]
        public string Username { get; set; }
    }
}