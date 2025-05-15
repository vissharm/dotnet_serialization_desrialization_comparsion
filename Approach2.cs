using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using ConsoleApp1;
using DeepEqual.Syntax;
using MessagePack;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach2
    {
        public static void RunApproach2(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach2_{threadCount}.txt");

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

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser2>(user, 500000);
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
            List<CleanUser2> deserializedUser;
            using (var file = File.OpenRead(filePath))
            {
                deserializedUser = MessagePack.MessagePackSerializer.Deserialize<List<CleanUser2>>(file);
                Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name}");
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
            //var deserializedUser = MessagePackSerializer.Deserialize<CleanUser2>(serializedData);
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
            // return new SerializationResult(serializationTime, deserializationTime);
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