using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using ConsoleApp1;
using DeepEqual.Syntax;
using MessagePack;
using MessagePack.Resolvers;
using Newtonsoft.Json;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach3
    {
        public static void RunApproach3(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach3_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser3
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile3
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address3
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order3>
                {
                    new Order3 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem3>(), TotalAmount = 345353 },
                    new Order3 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem3>(), TotalAmount = 345353 }
                },
                Address = new Address3
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
                SocialLinks = new List<SocialMedia3>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia3
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia3
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia3
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser3>(user, 1000000);
            Console.WriteLine("Done creating 10 lakh objects list");

            // Serialization to file
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            using (var file = File.Create(filePath))
            {
                MessagePack.MessagePackSerializer.Serialize(file, userlist);
            }
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            List<CleanUser3> deserializedUser;
            using (var file = File.OpenRead(filePath))
            {
                deserializedUser = MessagePack.MessagePackSerializer.Deserialize<List<CleanUser3>>(file);
                Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name}");
            }
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            //// Measure serialization and deserialization time
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //byte[] serializedData = MessagePackSerializer.Serialize(user, ContractlessStandardResolver.Options);
            //sw.Stop();
            //long serializationTime = sw.ElapsedMilliseconds;

            //sw.Restart();
            //var deserializedUser = MessagePackSerializer.Deserialize<CleanUser3>(serializedData, ContractlessStandardResolver.Options);
            //sw.Stop();
            //long deserializationTime = sw.ElapsedMilliseconds;

            //// Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");

            // Compare the original and deserialized lists
            //bool areListsEqual = DeepEqualityChecker.AreListsDeeplyEqual(userlist, deserializedUser);
            //Console.WriteLine($"Are the original and deserialized lists deeply equal? {areListsEqual}");
            bool areEqual = userlist.IsDeepEqual(deserializedUser);
            Console.WriteLine($"Are the lists deeply equal? {areEqual}");
            //SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            //return result;
            //return new SerializationResult(serializationTime, deserializationTime);
        }
    }

    //[DataContract]
    //public class CleanUser3
    //{
    //    [DataMember(Order = 1)]
    //    public int Id { get; set; }

    //    [DataMember(Order = 2)]
    //    public string Name { get; set; }

    //    public Profile3 Profile { get; set; }

    //    [DataMember(Order = 3)]
    //    public List<Order3> Orders { get; set; }

    //    [JsonIgnore] // Ignored during JSON serialization
    //    [IgnoreDataMember]
    //    public string Secret { get { return "Hello"; } }
    //}

    //[DataContract]
    //public class Profile3
    //{
    //    [DataMember(Order = 1)]
    //    public string Email { get; set; }

    //    [DataMember(Order = 2)]
    //    public string Phone { get; set; }

    //    [NonSerialized] // Ignored during binary serialization
    //    private string Secret = "hello";
    //}

    //[DataContract]
    //public class Order3
    //{
    //    [DataMember]
    //    public int OrderId { get; set; }

    //    [DataMember]
    //    public DateTime OrderDate { get; set; }
    //}

    [DataContract]
    public class CleanUser3
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public Profile3 Profile { get; set; }

        [DataMember(Order = 4)]
        public List<Order3> Orders { get; set; }

        [DataMember(Order = 5)]
        public Address3 Address { get; set; }

        [DataMember(Order = 6)]
        public Dictionary<string, string> Preferences { get; set; }

        [DataMember(Order = 7)]
        public List<int> FavoriteNumbers { get; set; }

        [DataMember(Order = 8)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Order = 9)]
        public List<SocialMedia3> SocialLinks { get; set; }

        [JsonIgnore] // Ignored during JSON serialization
        [IgnoreDataMember]
        public string Secret { get { return "This is private"; } }
    }

    [DataContract]
    public class Profile3
    {
        [DataMember(Order = 1)]
        public string Email { get; set; }

        [DataMember(Order = 2)]
        public string Phone { get; set; }

        [DataMember(Order = 3)]
        public DateTime BirthDate { get; set; }

        [DataMember(Order = 4)]
        public Address3 PrimaryAddress { get; set; }

        [NonSerialized] // Ignored during binary serialization
        private string InternalNote = "Internal Data";
    }

    [DataContract]
    public class Order3
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [DataMember(Order = 2)]
        public DateTime OrderDate { get; set; }

        [DataMember(Order = 3)]
        public decimal TotalAmount { get; set; }

        [DataMember(Order = 4)]
        public List<OrderItem3> Items { get; set; }
    }

    [DataContract]
    public class OrderItem3
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
    public class Address3
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
    public class SocialMedia3
    {
        [DataMember(Order = 1)]
        public string Platform { get; set; }

        [DataMember(Order = 2)]
        public string Url { get; set; }

        [DataMember(Order = 3)]
        public string Username { get; set; }
    }
}