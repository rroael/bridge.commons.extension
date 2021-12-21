using System.Collections.Generic;
using System.Linq;

namespace Bridge.Commons.Extension
{
    /// <summary>
    ///     Extensão de listas
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        ///     Chunk by
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<List<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
        {
            var result = source.Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList());

            return result;
        }

        /// <summary>
        ///     Chunk by
        /// </summary>
        /// <param name="source"></param>
        /// <param name="chunkSize"></param>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <returns></returns>
        public static IEnumerable<IDictionary<TK, TV>> ChunkBy<TK, TV>(this IDictionary<TK, TV> source, int chunkSize)
        {
            var result = source.Select((x, i) => new { Index = i, KeyValuePair = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.ToDictionary(k => k.KeyValuePair.Key,
                    v => v.KeyValuePair.Value));

            return result;
        }
    }
}