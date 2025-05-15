using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ConsoleApp1;
using DeepEqual.Syntax;
using MessagePack;
using MessagePack.Resolvers;
using Newtonsoft.Json;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach4
    {
        public static void RunApproach4(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach4_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser4
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile4
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address4
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order4>
                {
                    new Order4 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem4>(), TotalAmount = 345353 },
                    new Order4 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem4>(), TotalAmount = 345353 }
                },
                Address = new Address4
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
                SocialLinks = new List<SocialMedia4>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia4
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia4
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia4
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser4>(user, 500000);
            Console.WriteLine("Done creating 10 lakh objects list");

            // Serialization to file
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            using (var file = File.Create(filePath))
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(file, userlist);
            }
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            List<CleanUser4> deserializedUser;
            using (var file = File.OpenRead(filePath))
            {
                var formatter = new BinaryFormatter();
                deserializedUser = (List<CleanUser4>)formatter.Deserialize(file);
                Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name}");
            }
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            //// Measure serialization and deserialization time
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //var memoryStream = new MemoryStream();
            //var binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Serialize(memoryStream, user);
            //sw.Stop();
            //long serializationTime = sw.ElapsedMilliseconds;

            //sw.Restart();
            //memoryStream.Seek(0, SeekOrigin.Begin);
            //var deserializedObject = (CleanUser4)binaryFormatter.Deserialize(memoryStream);
            //sw.Stop();
            //long deserializationTime = sw.ElapsedMilliseconds;

            //// Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");

            //SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            //return result;
            // return new SerializationResult(serializationTime, deserializationTime);
            // Compare the original and deserialized lists
            //bool areListsEqual = DeepEqualityChecker.AreListsDeeplyEqual(userlist, deserializedUser);
            //Console.WriteLine($"Are the original and deserialized lists deeply equal? {areListsEqual}");
            bool areEqual = userlist.IsDeepEqual(deserializedUser);
            Console.WriteLine($"Are the lists deeply equal? {areEqual}");
        }
    }

    //[Serializable]
    //public class CleanUser4
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public Profile4 Profile { get; set; }

    //    public List<Order4> Orders { get; set; }

    //    private string Secret { get { return "Hello"; } }
    //}

    //[Serializable]
    //public class Profile4
    //{
    //    public string Email { get; set; }

    //    public string Phone { get; set; }

    //    [NonSerialized] // Ignored during binary serialization
    //    private string Secret = "hello";
    //}

    //[Serializable]
    //public class Order4
    //{
    //    public int OrderId { get; set; }

    //    public DateTime OrderDate { get; set; }
    //}

    [Serializable]
    public class CleanUser4
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Profile4 Profile { get; set; }

        public List<Order4> Orders { get; set; }

        public Address4 Address { get; set; }

        public Dictionary<string, string> Preferences { get; set; }

        public List<int> FavoriteNumbers { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<SocialMedia4> SocialLinks { get; set; }

        private string Secret { get { return "This is private"; } }
    }

    [Serializable]
    public class Profile4
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public Address4 PrimaryAddress { get; set; }

        [NonSerialized] // Ignored during binary serialization
        private string InternalNote = "Internal Data";
    }

    [Serializable]
    public class Order4
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItem4> Items { get; set; }
    }

    [Serializable]
    public class OrderItem4
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }

    [Serializable]
    public class Address4
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }
    }

    [Serializable]
    public class SocialMedia4
    {
        public string Platform { get; set; }

        public string Url { get; set; }

        public string Username { get; set; }
    }
}