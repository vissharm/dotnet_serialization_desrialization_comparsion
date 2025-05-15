using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDeserializationComparsionDemo
{
    public class util
    {
        // Generic method to create a list of replicated objects
        //public static List<T> CreateReplicatedList<T>(T singleObject, int n)
        //{
        //    if (n < 0)
        //    {
        //        throw new ArgumentException("The size of the list must be non-negative.", nameof(n));
        //    }

        //    var list = new List<T>(n);

        //    for (int i = 0; i < n; i++)
        //    {
        //        list.Add(singleObject);
        //    }

        //    return list;
        //}

        public static List<T> CreateReplicatedList<T>(T singleObject, int n)
        {
            if (n < 0)
                throw new ArgumentException("The size of the list must be non-negative.", nameof(n));

            var list = new List<T>(n);
            for (int i = 0; i < n; i++)
            {
                list.Add(DeepCopy(singleObject));
            }
            return list;
        }

        // Reflection-based DeepCopy method that handles nested and complex types
        public static T DeepCopy<T>(T obj)
        {
            if (obj == null) return default;

            // Handle value types and strings directly
            if (obj.GetType().IsValueType || obj is string)
            {
                return obj;
            }

            // Handle arrays
            if (obj.GetType().IsArray)
            {
                var array = obj as Array;
                var elementType = obj.GetType().GetElementType();
                var copiedArray = Array.CreateInstance(elementType, array.Length);

                for (int i = 0; i < array.Length; i++)
                {
                    copiedArray.SetValue(DeepCopy(array.GetValue(i)), i);
                }

                return (T)(object)copiedArray;
            }

            // Handle collections (List, Dictionary, etc.)
            if (obj is IList list)
            {
                var listType = obj.GetType();
                var copiedList = (IList)Activator.CreateInstance(listType);

                foreach (var item in list)
                {
                    copiedList.Add(DeepCopy(item));
                }

                return (T)copiedList;
            }

            if (obj is IDictionary dictionary)
            {
                var dictType = obj.GetType();
                var copiedDict = (IDictionary)Activator.CreateInstance(dictType);

                foreach (var key in dictionary.Keys)
                {
                    var value = dictionary[key];
                    copiedDict.Add(DeepCopy(key), DeepCopy(value));
                }

                return (T)copiedDict;
            }

            // Handle objects
            var type = obj.GetType();
            var copy = Activator.CreateInstance(type);

            // Copy all fields
            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                var fieldValue = field.GetValue(obj);
                field.SetValue(copy, DeepCopy(fieldValue));
            }

            // Copy all properties
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (property.CanRead && property.CanWrite)
                {
                    var propertyValue = property.GetValue(obj);
                    property.SetValue(copy, DeepCopy(propertyValue));
                }
            }

            return (T)copy;
        }
    }
}
