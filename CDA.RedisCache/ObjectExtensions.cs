using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CDA.RedisCache
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Gets the value of the given property as a string.
        /// </summary>
        /// <param name="o">Object to get the property from.</param>
        /// <param name="propertyName">Name of property whose value to get.</param>
        /// <returns>Value of property as a string.</returns>
        public static string GetPropertyValueAsString(this object o, string propertyName)
        {
            Type type = o.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            if (propertyInfo == null)
            {
                return null;
            }

            return propertyInfo.GetValue(o) as string;
        }

        /// <summary>
        /// Returns the given object as a compressed byte array.
        /// </summary>
        /// <param name="o">The object to compress.</param>
        /// <returns>object as a compressed (deflated) byte array.</returns>
        public static byte[] CompressAndSerialize(this object o)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (var compressedStream = new MemoryStream())
            {
                using (var uncompressedStream = new MemoryStream())
                {
                    using (var jsonTextWriter = new JsonTextWriter(new StreamWriter(uncompressedStream)))
                    {
                        serializer.Serialize(jsonTextWriter, o);
                        jsonTextWriter.Flush();
                        uncompressedStream.Position = 0;

                        using (var compressorStream = new DeflateStream(compressedStream, CompressionLevel.Fastest, true))
                        {
                            uncompressedStream.CopyTo(compressorStream);
                        }
                    }
                }

                return compressedStream.ToArray();
            }

        }
    }

}
