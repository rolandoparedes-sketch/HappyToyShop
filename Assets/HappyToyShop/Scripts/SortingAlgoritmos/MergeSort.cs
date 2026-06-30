using System;

namespace HappyToyShop.Collections.Sorting
{
    // MERGE SORT — "Ordenamiento por mezcla"
    // Nombre: divide el arreglo en mitades hasta tener subarreglos de 1 elemento
    // (trivialmente ordenados) y luego los MEZCLA (merge) de vuelta en orden.
    // Estrategia: Divide y Vencerás (Divide & Conquer).
    //
    // Cuándo usarlo:
    //   ✓ Cuando se necesita rendimiento GARANTIZADO — siempre O(n log n)
    //   ✓ Ordenamiento estable para datos con múltiples claves
    //   ✓ Muy útil para listas enlazadas
    //   ✗ Requiere memoria adicional O(n)
    //
    // Complejidad:
    //   Mejor caso:   O(n log n)
    //   Caso promedio:O(n log n)
    //   Peor caso:    O(n log n)
    //   Espacio:      O(n)
    //   Estable:      Sí
    public static class MergeSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable<T>
        {
            if (arr == null || arr.Length <= 1)
                return;

            SortRecursive(arr, 0, arr.Length - 1);
        }

        private static void SortRecursive<T>(T[] arr, int left, int right)
            where T : IComparable<T>
        {
            // Caso base: un único elemento ya está ordenado
            if (left >= right)
                return;

            int mid = left + (right - left) / 2;

            // Ordenar mitad izquierda
            SortRecursive(arr, left, mid);

            // Ordenar mitad derecha
            SortRecursive(arr, mid + 1, right);

            // Fusionar ambas mitades ordenadas
            Merge(arr, left, mid, right);
        }

        // Fusiona dos subarreglos ordenados:
        // arr[left..mid]
        // arr[mid+1..right]
        private static void Merge<T>(T[] arr, int left, int mid, int right)
            where T : IComparable<T>
        {
            int leftSize = mid - left + 1;
            int rightSize = right - mid;

            // Crear arreglos temporales
            T[] leftArr = new T[leftSize];
            T[] rightArr = new T[rightSize];

            // Copiar datos
            Array.Copy(arr, left, leftArr, 0, leftSize);
            Array.Copy(arr, mid + 1, rightArr, 0, rightSize);

            int i = 0;      // índice izquierda
            int j = 0;      // índice derecha
            int k = left;   // índice arreglo original

            // Mezclar comparando elementos de ambas mitades
            while (i < leftSize && j < rightSize)
            {
                // <= mantiene estabilidad
                if (leftArr[i].CompareTo(rightArr[j]) <= 0)
                {
                    arr[k] = leftArr[i];
                    i++;
                }
                else
                {
                    arr[k] = rightArr[j];
                    j++;
                }

                k++;
            }

            // Copiar elementos restantes de la izquierda
            while (i < leftSize)
            {
                arr[k] = leftArr[i];
                i++;
                k++;
            }

            // Copiar elementos restantes de la derecha
            while (j < rightSize)
            {
                arr[k] = rightArr[j];
                j++;
                k++;
            }
        }

        // Ejemplo de uso
        public static string[] UseCaseDemo()
        {
            string[] employees =
            {
                "Carlos-IT",
                "Ana-HR",
                "Bob-IT",
                "Diana-HR",
                "Eve-IT"
            };

            Sort(employees);

            return employees;
        }
    }
}