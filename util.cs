using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerializationDeserializationComparsionDemo
{
    public class util
    {
        // Generic method to create a list of replicated objects
        public static List<T> CreateReplicatedList<T>(T singleObject, int n)
        {
            if (n < 0)
            {
                throw new ArgumentException("The size of the list must be non-negative.", nameof(n));
            }

            var list = new List<T>(n);

            for (int i = 0; i < n; i++)
            {
                list.Add(singleObject);
            }

            return list;
        }
    }
}
