﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using MessagePack;
using MessagePack.Resolvers;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json;
using ConsoleApp1;
using DeepEqual.Syntax;

namespace SerializationDeserializationComparsionDemo
{
    public class Approach7
    {
        public static void RunApproach7(int threadCount)
        {
            string sourceDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(sourceDirectory,  $"approach7_{threadCount}.txt");

            // Create sample data
            var user = new CleanUser7
            {
                Id = 123,
                Name = "John Doe",
                Profile = new Profile7
                {
                    Email = "john@example.com",
                    Phone = "123-456-7890",
                    BirthDate = DateTime.Now,
                    PrimaryAddress = new Address7
                    {
                        City = "dummy",
                        Country = "india",
                        State = "punjab",
                        Street = "hell",
                        ZipCode = "000898"
                    }
                },
                Orders = new List<Order7>
                {
                    new Order7 { OrderId = 1, OrderDate = DateTime.UtcNow, Items = new List<OrderItem7>(), TotalAmount = 345353 },
                    new Order7 { OrderId = 2, OrderDate = DateTime.UtcNow.AddDays(-1), Items = new List<OrderItem7>(), TotalAmount = 345353 }
                },
                Address = new Address7
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
                SocialLinks = new List<SocialMedia7>()
            };

            user.Preferences.Add("sdsds", "dfdfdfd");
            user.Preferences.Add("sdegssds", "dfdfdfd");
            user.Preferences.Add("effsfs", "dfdfdfd");
            user.SocialLinks.Add(new SocialMedia7
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia7
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });
            user.SocialLinks.Add(new SocialMedia7
            {
                Platform = "sdfsdfdsfs",
                Url = "www.sdfsdf.com",
                Username = "mainhundon"
            });

            Console.WriteLine("Creating 10 lakh objects list");
            var userlist = util.CreateReplicatedList<CleanUser7>(user, 500000);
            Console.WriteLine("Done creating 10 lakh objects list");

            // Serialization to file
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(userlist, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            });
            File.WriteAllText(filePath, jsonString);
            stopwatch.Stop();
            long serializationTime = stopwatch.ElapsedMilliseconds;

            // Deserialization from file
            stopwatch.Restart();
            jsonString = File.ReadAllText(filePath);
            var deserializedUser = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CleanUser7>>(jsonString, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
            });
            stopwatch.Stop();
            long deserializationTime = stopwatch.ElapsedMilliseconds;

            //// Measure serialization and deserialization time
            //Stopwatch sw = new Stopwatch();

            //sw.Start();
            //var serializedData = JsonConvert.SerializeObject(user);
            //sw.Stop();
            //long serializationTime = sw.ElapsedMilliseconds;

            //sw.Restart();
            //var deserializedObject = JsonConvert.DeserializeObject<CleanUser6>(serializedData);
            //sw.Stop();
            //long deserializationTime = sw.ElapsedMilliseconds;

            //// Output analytics
            Console.WriteLine($"Deserialized User Name: {deserializedUser[0].Name}");
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

    //public class CleanUser7
    //{
    //    public int Id { get; set; }

    //    [JsonProperty("name")]
    //    public string Name { get; set; }

    //    public Profile7 Profile { get; set; }

    //    public List<Order7> Orders { get; set; }

    //    [Newtonsoft.Json.JsonIgnore] // Ignored during JSON serialization
    //    public string Secret { get { return "Hello"; } }
    //}

    //public class Profile7
    //{
    //    public string Email { get; set; }

    //    public string Phone { get; set; }

    //    private string Secret = "hello";
    //}

    //public class Order7
    //{
    //    public int OrderId { get; set; }

    //    public DateTime OrderDate { get; set; }
    //}

    public class CleanUser7
    {
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public Profile7 Profile { get; set; }

        public List<Order7> Orders { get; set; }

        public Address7 Address { get; set; }

        public Dictionary<string, string> Preferences { get; set; }

        public List<int> FavoriteNumbers { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<SocialMedia7> SocialLinks { get; set; }

        [Newtonsoft.Json.JsonIgnore] // Ignored during JSON serialization
        public string Secret { get { return "This is private"; } }
    }

    public class Profile7
    {
        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime BirthDate { get; set; }

        public Address7 PrimaryAddress { get; set; }

        private string InternalNote = "Internal Data";
    }

    public class Order7
    {
        public int OrderId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItem7> Items { get; set; }
    }

    public class OrderItem7
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }

    public class Address7
    {
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string Country { get; set; }
    }

    public class SocialMedia7
    {
        public string Platform { get; set; }

        public string Url { get; set; }

        public string Username { get; set; }
    }
}