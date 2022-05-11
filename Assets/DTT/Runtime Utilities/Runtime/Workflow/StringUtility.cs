using System;
using Random = UnityEngine.Random;

namespace DTT.Utils.Workflow
{
    /// <summary>
    /// Provides utility methods for strings.
    /// </summary>
    public static class StringUtility
    {
        /// <summary>
        /// The default random state used for random generation.
        /// </summary>
        private static readonly Random.State defaultState = Random.state;

        /// <summary>
        /// Generates a random insecure string which can be used when needing dummy data to test.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>The random insecure string.</returns>
        public static string RandomInsecure(int length) => RandomInsecure(length, null);

        /// <summary>
        /// Generates a random insecure string which can be used when needing dummy data to test.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <param name="seed">The seed to initialize the 'Random' state with.</param>
        /// <returns>The random insecure string.</returns>
        public static string RandomInsecure(int length, int? seed)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            // Use the seed if it has a value. Otherwise use the default state.
            if (seed.HasValue)
                Random.InitState(seed.Value);
            else
                Random.state = defaultState;
            
            const string SELECTION = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            
            char[] result = new char[length];
            for (int i = 0; i < result.Length; i++)
                result[i] = SELECTION[Random.Range(0, SELECTION.Length)];

            return new string(result);
        }
    }
}