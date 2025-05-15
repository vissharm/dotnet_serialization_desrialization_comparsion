using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.ConstrainedExecution;
using Ceras;
using ConsoleApp1;
using DeepEqual.Syntax;
using MessagePack;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach8
    {
        public static void RunApproach8(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach8_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser8
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile8
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address8
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order8>
                {
                    new Order8 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem8>(), TotalAmount = 345353 },
                    new Order8 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem8>(), TotalAmount = 345353 }
                },
                Address = new Address8
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
                SocialLinks = new List<SocialMedia8>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia8
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia8
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia8
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser8>(user, 500000);
            Console.WriteLine("Done creating 10 lakh objects list");

            // Serialization to file
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var config = new SerializerConfig();
            config.PreserveReferences = true;
            config.DefaultTargets = TargetMember.AllPublic;
            var ceras = new Ceras.CerasSerializer(config);
            // var serializedData = ceras.Serialize(userlist);

            // Byte array for serialization
            byte[] buffer = null;
            // Serialize the object into the byte array
            ceras.Serialize(userlist, ref buffer);


            File.WriteAllBytes(filePath, buffer);
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            var serializedBytes = File.ReadAllBytes(filePath);
            List<CleanUser8> deserializedUser = null;
            ceras.Deserialize<List<CleanUser8>>(ref deserializedUser, serializedBytes);
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            //// Measure serialization and deserialization time
            //Stopwatch sw = new Stopwatch();
            //sw.Start();

            //var ceras = new CerasSerializer();
            //var data = ceras.Serialize(user);
            //sw.Stop();
            //long serializationTime = sw.ElapsedMilliseconds;

            //sw.Restart();
            //var deserializedUser = ceras.Deserialize<CleanUser8>(data);
            //sw.Stop();
            //long deserializationTime = sw.ElapsedMilliseconds;

            //// Output analytics
            Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name} | {deserializedUser.Count}");
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");

            // Compare the original and deserialized lists
            //bool areListsEqual = DeepEqualityChecker.AreListsDeeplyEqual(userlist, deserializedUser);
            //Console.WriteLine($"Are the original and deserialized lists deeply equal? {areListsEqual}");

            //SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            //return result;
            //return new SerializationResult(serializationTime, deserializationTime);
            bool areEqual = userlist.IsDeepEqual(deserializedUser);
            Console.WriteLine($"Are the lists deeply equal? {areEqual}");
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
    public class CleanUser8
    {
        [Key(0)]
        public int Id { get; set; }

        [Key(1)]
        public string Name { get; set; }

        [Key(2)]
        public Profile8 Profile { get; set; }

        [Key(3)]
        public List<Order8> Orders { get; set; }

        [Key(4)]
        public Address8 Address { get; set; }

        [Key(5)]
        public Dictionary<string, string> Preferences { get; set; }

        [Key(6)]
        public List<int> FavoriteNumbers { get; set; }

        [Key(7)]
        public DateTime CreatedAt { get; set; }

        [Key(8)]
        public List<SocialMedia8> SocialLinks { get; set; }

        [IgnoreMember]
        public string Secret { get { return "This is private"; } }
    }

    [MessagePackObject]
    public class Profile8
    {
        [Key(0)]
        public string Email { get; set; }

        [Key(1)]
        public string Phone { get; set; }

        [Key(2)]
        public DateTime BirthDate { get; set; }

        [Key(3)]
        public Address8 PrimaryAddress { get; set; }

        [IgnoreMember]
        private string InternalNote = "Internal Data";
    }

    [MessagePackObject]
    public class Order8
    {
        [Key(0)]
        public int OrderId { get; set; }

        [Key(1)]
        public DateTime OrderDate { get; set; }

        [Key(2)]
        public decimal TotalAmount { get; set; }

        [Key(3)]
        public List<OrderItem8> Items { get; set; }
    }

    [MessagePackObject]
    public class OrderItem8
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
    public class Address8
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
    public class SocialMedia8
    {
        [Key(0)]
        public string Platform { get; set; }

        [Key(1)]
        public string Url { get; set; }

        [Key(2)]
        public string Username { get; set; }
    }
}