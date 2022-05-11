using System.Collections.Generic;

namespace DTT.Utils.Extensions
{
    /// <summary>
    /// Provides extensions methods for classes that implement the <see cref="IList{T}"/> interface.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Removes null entries from a list.
        /// </summary>
        /// <typeparam name="T">The type of list.</typeparam>
        /// <param name="list">The list to remove null entries from.</param>
        public static void RemoveNullEntries<T>(this IList<T> list) where T : class
        {
            for (int i = list.Count - 1; i >= 0; i--)
                if (Equals(list[i], null))
                    list.RemoveAt(i);
        }

        /// <summary>
        /// Removes default values from a list.
        /// </summary>
        /// <typeparam name="T">The type of list.</typeparam>
        /// <param name="list">The list to remove default values from.</param>
        public static void RemoveDefaultValues<T>(this IList<T> list)
        {
            for (int i = list.Count - 1; i >= 0; i--)
                if (Equals(default(T), list[i]))
                    list.RemoveAt(i);
        }

        /// <summary>
        /// Returns whether an index is inside the bounds of the list.
        /// </summary>
        /// <typeparam name="T">The type of list to check the bounds of.</typeparam>
        /// <param name="list">The list to check the bounds of.</param>
        /// <param name="index">The index to check.</param>
        /// <returns>Whether the index is inside the bounds.</returns>
        public static bool HasIndex<T>(this IList<T> list, int index) => index.InRange(0, list.Count - 1);
    }
}