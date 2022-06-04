using System;

namespace Txtai{

    /// <summary>
    /// The TxtAI API.
    /// The interface that represents a serializer.
    /// </summary>
    public interface IApiSerializer
    {
        /// <summary>
        /// Serializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The serialized object.</returns>
        string Serialize(object obj);

        /// <summary>
        /// Deserializes the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>The deserialized object.</returns>
        object Deserialize(string obj);

        /// <summary>
        /// Deserializes the specified object using the specified type.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="type">The type.</param>
        /// <returns>The deserialized object.</returns>
        object Deserialize(string obj, Type type);

        /// <summary>
        /// Deserializes the specified object using the argument generic type.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns>The deserialized object.</returns>
        T Deserialize<T>(string obj);
    }
}