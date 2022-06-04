using System;
using System.Text.Json;

namespace Txtai{

    /// <summary>
    /// The TxtAI API.
    /// Represents basic implementation of json serializer.
    /// Using the System.Text.Json library.
    /// </summary>
    public class JsonApiSerializer : IApiSerializer
    {
        /// <summary>
        /// Deserialize the json to the specified type.
        /// </summary>
        public object Deserialize(string src) => JsonSerializer.Deserialize<object>(src);

        /// <summary>
        /// Deserialize the json to the specified type.
        /// </summary>
        /// <param name="src">The json string.</param>
        /// <param name="type">The type to deserialize.</param>
        public object Deserialize(string src, Type type) => JsonSerializer.Deserialize(src, type);
        
        /// <summary>
        /// Deserialize the json to the specified type.
        public T Deserialize<T>(string src) => JsonSerializer.Deserialize<T>(src);

        /// <summary>
        /// Serialize the object to json.
        /// </summary>
        /// <param name="target">The object to serialize.</param>
        /// <returns>The json string.</returns>
        public string Serialize(object target) => JsonSerializer.Serialize(target);
    }
}