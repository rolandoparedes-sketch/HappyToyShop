using System;

namespace HappyToyShop.Collections.Sorting
{
    // SELECTION SORT — "Ordenamiento por selección"
    // Nombre: en cada pasada SELECCIONA el elemento mínimo del subarreglo no ordenado
    // y lo coloca en su posición correcta. Divide el arreglo en: [ordenado | no ordenado].
    //
    // Cuándo usarlo:
    //   ✓ Cuando el costo de escritura (swap) es muy alto — hace exactamente n-1 swaps siempre
    //   ✓ Arreglos pequeños donde la simplicidad importa más que la velocidad
    //   ✗ Arreglos grandes, no se beneficia de datos ya ordenados
    //   ✗ No es estable (puede alterar el orden relativo de elementos iguales)
    //
    // Mejor: O(n²) | Promedio: O(n²) | Peor: O(n²) | Espacio: O(1) | Estable: No
    public static class SelectionSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable<T>
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                // busca el índice del mínimo en el subarreglo no ordenado [i..n-1]
                int minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j].CompareTo(arr[minIndex]) < 0)
                        minIndex = j;
                }

                // coloca el mínimo encontrado al inicio del subarreglo no ordenado
                if (minIndex != i)
                    (arr[i], arr[minIndex]) = (arr[minIndex], arr[i]);
            }
        }

        // Caso de uso: ordenar tarjetas de memoria flash donde cada escritura desgasta el chip
        // Selection hace solo n-1 escrituras sin importar el estado inicial
        public static int[] UseCaseDemo()
        {
            int[] flashMemorySlots = { 7, 2, 9, 1, 5, 3 };
            Sort(flashMemorySlots);
            return flashMemorySlots; // → 1 2 3 5 7 9  (con solo 5 swaps para 6 elementos)
        }
    }
}