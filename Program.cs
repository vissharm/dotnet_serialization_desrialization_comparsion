using System;
using System.Collections.Generic;
using MessagePackSerializationDemo;

namespace ConsoleApp1
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting MessagePack Serialization and Deserialization Analytics...\n");

            // Collecting results in a list for tabular display
            var results = new List<SerializationResult>();

            var approaches = new List<Func<SerializationResult>>()
            {
                Approach1.RunApproach1,
                Approach2.RunApproach2,
                Approach3.RunApproach3,
                Approach4.RunApproach4,
                Approach5.RunApproach5,
                Approach6.RunApproach6,
                Approach7.RunApproach7,
                Approach8.RunApproach8
            };

            var approachNames = new List<string>
            {
                "Approach 1 (MP + Existing Annotations)",
                "Approach 2 (Optimized MessagePack)",
                "Approach 3 (No MessagePack annotation)",
                "Approach 4 (Binary Formatter)",
                "Approach 5 (System.Text.Json)",
                "Approach 6 (DataContract)",
                "Approach 7 (Newtonsoft.Json)",
                "Approach 8 (Ceras)"
            };

            for (int i = 0; i < approaches.Count; i++)
            {
                Console.WriteLine($"\nExecuting {approachNames[i]}...");

                long totalSerializationTime = 0;
                long totalDeserializationTime = 0;

                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine($"Run {j + 1}...");
                    var result = approaches[i]();
                    totalSerializationTime += result.SerializationTime;
                    totalDeserializationTime += result.DeserializationTime;
                }

                long avgSerializationTime = totalSerializationTime / 4;
                long avgDeserializationTime = totalDeserializationTime / 4;

                results.Add(new SerializationResult(approachNames[i], avgSerializationTime, avgDeserializationTime));
            }

            // Display the results in a table format
            PrintResultsTable(results);

            Console.WriteLine("\nAnalytics Complete!");
        }

        private static void PrintResultsTable(List<SerializationResult> results)
        {
            // Find the fastest and smallest serialization and deserialization times
            var fastestSerialization = results[0];
            var fastestDeserialization = results[0];

            foreach (var result in results)
            {
                if (result.SerializationTime < fastestSerialization.SerializationTime)
                    fastestSerialization = result;

                if (result.DeserializationTime < fastestDeserialization.DeserializationTime)
                    fastestDeserialization = result;
            }

            // Print the table header
            Console.WriteLine("\n| Approach                              | Avg Serialization Time (ms) | Avg Deserialization Time (ms) |");
            Console.WriteLine("|---------------------------------------|-----------------------------|------------------------------|");

            // Print each result row
            foreach (var result in results)
            {
                var serializationHighlight = result == fastestSerialization ? "(Fastest)" : "";
                var deserializationHighlight = result == fastestDeserialization ? "(Fastest)" : "";

                Console.WriteLine(
                    $"| {result.Approach,-37} | {result.SerializationTime,27} {serializationHighlight,-10} | {result.DeserializationTime,28} {deserializationHighlight,-10} |");
            }
        }
    }

    public class SerializationResult
    {
        public string Approach { get; set; }
        public long SerializationTime { get; set; }
        public long DeserializationTime { get; set; }

        public SerializationResult(string approach, long serializationTime, long deserializationTime)
        {
            Approach = approach;
            SerializationTime = serializationTime;
            DeserializationTime = deserializationTime;
        }

        public SerializationResult(long serializationTime, long deserializationTime)
        {
            SerializationTime = serializationTime;
            DeserializationTime = deserializationTime;
        }
    }
}