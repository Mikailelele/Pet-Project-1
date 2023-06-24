namespace PepegaAR.Utils
{
    using System;
    using System.Runtime.CompilerServices;

    public static class Utils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetNextOrFirst<T>(this T[] array, T previous)
        {
            int length = array.Length;

            if(length == 0)
            {
                throw new InvalidOperationException($"{nameof(array)} is empty.");
            }

            int currentIndex = Array.IndexOf(array, previous);
            int nextIndex = (currentIndex + 1) % length;

            return array[nextIndex];
        }
    }
}
