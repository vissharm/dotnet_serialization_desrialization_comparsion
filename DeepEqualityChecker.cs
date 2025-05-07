using System;
using System.Collections.Generic;
using System.Text.Json;

namespace SerializationDeserializationComparsionDemo
{
    public static class DeepEqualityChecker
    {
        /// <summary>
        /// Checks whether the original list of objects before serialization is deeply equal to the deserialized list of objects.
        /// </summary>
        /// <typeparam name="T">The type of objects in the lists.</typeparam>
        /// <param name="originalList">The original list before serialization.</param>
        /// <param name="deserializedList">The deserialized list after deserialization.</param>
        /// <returns>True if the lists and their objects are deeply equal; otherwise, false.</returns>
        public static bool AreListsDeeplyEqual<T>(List<T> originalList, List<T> deserializedList)
        {
            if (originalList == null || deserializedList == null)
            {
                return originalList == null && deserializedList == null;
            }

            if (originalList.Count != deserializedList.Count)
            {
                return false; // Lists are of different lengths, so they can't be equal.
            }

            for (int i = 0; i < originalList.Count; i++)
            {
                // Serialize each object in the lists to JSON for deep comparison
                string originalJson = JsonSerializer.Serialize(originalList[i], new JsonSerializerOptions
                {
                    WriteIndented = false,
                    IgnoreNullValues = false
                });

                string deserializedJson = JsonSerializer.Serialize(deserializedList[i], new JsonSerializerOptions
                {
                    WriteIndented = false,
                    IgnoreNullValues = false
                });

                // Compare serialized JSON representations
                if (originalJson != deserializedJson)
                {
                    return false; // An individual object doesn't match
                }
            }

            return true; // All objects in the lists are deeply equal
        }
    }
}
