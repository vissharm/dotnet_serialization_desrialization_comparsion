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
using DeepEqual.Syntax;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach6
    {
        public static void RunApproach6(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach6_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser6
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile6
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address6
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order6>
                {
                    new Order6 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem6>(), TotalAmount = 345353 },
                    new Order6 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem6>(), TotalAmount = 345353 }
                },
                Address = new Address6
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
                SocialLinks = new List<SocialMedia6>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia6
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia6
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia6
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser6>(user, 500000);
            Console.WriteLine("Done creating 10 lakh objects list");

            // Serialization to file
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            using (var file = File.Create(filePath))
            {
                var serializer = new DataContractSerializer(typeof(List<CleanUser6>));
                serializer.WriteObject(file, userlist);
            }
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            List<CleanUser6> deserializedUser;
            using (var file = File.OpenRead(filePath))
            {
                var serializer = new DataContractSerializer(typeof(List<CleanUser6>));
                deserializedUser = (List<CleanUser6>)serializer.ReadObject(file);
                Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name}");
            }
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            //// Measure serialization and deserialization time
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //var memoryStream = new MemoryStream();
            //var dataContractSerializer = new DataContractSerializer(typeof(CleanUser6));
            //dataContractSerializer.WriteObject(memoryStream, user);
            //sw.Stop();
            //long serializationTime = sw.ElapsedMilliseconds;

            //sw.Restart();
            //memoryStream.Seek(0, SeekOrigin.Begin);
            //var deserializedObject = (CleanUser6)dataContractSerializer.ReadObject(memoryStream);
            //sw.Stop();
            //long deserializationTime = sw.ElapsedMilliseconds;

            //// Output analytics
            Console.WriteLine($"Serialization Time: {serializationTime} ms");
            Console.WriteLine($"Deserialization Time: {deserializationTime} ms");

            //SerializationResult result = new SerializationResult(serializationTime, deserializationTime);
            //return result;
            //return new SerializationResult(serializationTime, deserializationTime);
            // Compare the original and deserialized lists
            //bool areListsEqual = DeepEqualityChecker.AreListsDeeplyEqual(userlist, deserializedUser);
            //Console.WriteLine($"Are the original and deserialized lists deeply equal? {areListsEqual}");
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
    public class CleanUser6
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string Name { get; set; }

        [DataMember(Order = 3)]
        public Profile6 Profile { get; set; }

        [DataMember(Order = 4)]
        public List<Order6> Orders { get; set; }

        [DataMember(Order = 5)]
        public Address6 Address { get; set; }

        [DataMember(Order = 6)]
        public Dictionary<string, string> Preferences { get; set; }

        [DataMember(Order = 7)]
        public List<int> FavoriteNumbers { get; set; }

        [DataMember(Order = 8)]
        public DateTime CreatedAt { get; set; }

        [DataMember(Order = 9)]
        public List<SocialMedia6> SocialLinks { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public string Secret { get { return "This is private"; } }
    }

    [DataContract]
    public class Profile6
    {
        [DataMember(Order = 1)]
        public string Email { get; set; }

        [DataMember(Order = 2)]
        public string Phone { get; set; }

        [DataMember(Order = 3)]
        public DateTime BirthDate { get; set; }

        [DataMember(Order = 4)]
        public Address6 PrimaryAddress { get; set; }

        [IgnoreDataMember]
        private string InternalNote = "Internal Data";
    }

    [DataContract]
    public class Order6
    {
        [DataMember(Order = 1)]
        public int OrderId { get; set; }

        [DataMember(Order = 2)]
        public DateTime OrderDate { get; set; }

        [DataMember(Order = 3)]
        public decimal TotalAmount { get; set; }

        [DataMember(Order = 4)]
        public List<OrderItem6> Items { get; set; }
    }

    [DataContract]
    public class OrderItem6
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
    public class Address6
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
    public class SocialMedia6
    {
        [DataMember(Order = 1)]
        public string Platform { get; set; }

        [DataMember(Order = 2)]
        public string Url { get; set; }

        [DataMember(Order = 3)]
        public string Username { get; set; }
    }
}