using System;

namespace HappyToyShop.Collections.Sorting
{
    // QUICK SORT — "Ordenamiento rápido"
    // Mejor: O(n log n)
    // Promedio: O(n log n)
    // Peor: O(n²)
    // Espacio: O(log n)
    // Estable: No
    public static class QuickSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable<T>
        {
            if (arr == null || arr.Length <= 1)
                return;

            SortRecursive(arr, 0, arr.Length - 1);
        }

        private static void SortRecursive<T>(T[] arr, int low, int high)
            where T : IComparable<T>
        {
            if (low >= high)
                return;

            // Para subarreglos pequeños es más eficiente usar Insertion Sort
            if (high - low < 10)
            {
                InsertionSortSubarray(arr, low, high);
                return;
            }

            int pivotIndex = Partition(arr, low, high);

            SortRecursive(arr, low, pivotIndex - 1);
            SortRecursive(arr, pivotIndex + 1, high);
        }

        private static int Partition<T>(T[] arr, int low, int high)
            where T : IComparable<T>
        {
            int mid = low + (high - low) / 2;

            MedianOfThree(arr, low, mid, high);

            T pivot = arr[mid];

            (arr[mid], arr[high - 1]) = (arr[high - 1], arr[mid]);

            int pivotPos = high - 1;

            int i = low;
            int j = pivotPos - 1;

            while (true)
            {
                while (arr[++i].CompareTo(pivot) < 0) { }

                while (j > low && arr[--j].CompareTo(pivot) > 0) { }

                if (i >= j)
                    break;

                (arr[i], arr[j]) = (arr[j], arr[i]);
            }

            (arr[i], arr[pivotPos]) = (arr[pivotPos], arr[i]);

            return i;
        }

        private static void MedianOfThree<T>(T[] arr, int a, int b, int c)
            where T : IComparable<T>
        {
            if (arr[a].CompareTo(arr[b]) > 0)
                (arr[a], arr[b]) = (arr[b], arr[a]);

            if (arr[a].CompareTo(arr[c]) > 0)
                (arr[a], arr[c]) = (arr[c], arr[a]);

            if (arr[b].CompareTo(arr[c]) > 0)
                (arr[b], arr[c]) = (arr[c], arr[b]);
        }

        private static void InsertionSortSubarray<T>(T[] arr, int low, int high)
            where T : IComparable<T>
        {
            for (int i = low + 1; i <= high; i++)
            {
                T key = arr[i];
                int j = i - 1;

                while (j >= low && arr[j].CompareTo(key) > 0)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key;
            }
        }

        // Caso de uso:
        // Ordenar entidades por distancia al jugador
        public static float[] UseCaseDemo()
        {
            float[] distances =
            {
                12.5f,
                3.1f,
                8.9f,
                1.2f,
                15.0f,
                6.7f,
                4.3f,
                11.1f
            };

            Sort(distances);

            return distances;
        }
    }
}