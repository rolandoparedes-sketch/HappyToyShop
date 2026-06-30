using System;

namespace HappyToyShop.Collections.Sorting
{
    // HEAP SORT — "Ordenamiento por montículo"
    // Nombre: utiliza una estructura de datos llamada HEAP (montículo/pirámide) —
    // un árbol binario casi completo donde cada padre es mayor que sus hijos (max-heap).
    // El máximo siempre está en la raíz, lo extrae y reconstruye el heap.
    //
    // Cuándo usarlo:
    //   ✓ Cuando se necesita O(n log n) GARANTIZADO con O(1) espacio extra (a diferencia de MergeSort)
    //   ✓ Sistemas embebidos con memoria muy limitada
    //   ✓ Priority queues en tiempo real
    //   ✗ No es estable
    //   ✗ Peor rendimiento en caché que QuickSort (acceso no secuencial a memoria)
    //
    // Mejor: O(n log n) | Promedio: O(n log n) | Peor: O(n log n) | Espacio: O(1) | Estable: No
    public static class HeapSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable<T>
        {
            int n = arr.Length;

            // Fase 1 — Build Max-Heap: convierte el arreglo en un max-heap
            // comienza desde el último nodo con hijos (n/2 - 1) y sube hasta la raíz
            for (int i = n / 2 - 1; i >= 0; i--)
                Heapify(arr, n, i);

            // Fase 2 — Extract: extrae el máximo (raíz) y repara el heap n-1 veces
            for (int i = n - 1; i > 0; i--)
            {
                // mueve la raíz (máximo actual) al final del arreglo no ordenado
                (arr[0], arr[i]) = (arr[i], arr[0]);

                // repara el heap en el subarreglo reducido arr[0..i-1]
                Heapify(arr, i, 0);
            }
        }

        // Heapify: asegura que el subárbol con raíz en 'root' cumple la propiedad max-heap
        // 'size' es el tamaño del heap activo (el resto ya está ordenado al final)
        private static void Heapify<T>(T[] arr, int size, int root) where T : IComparable<T>
        {
            int largest = root;       // asume que la raíz es el mayor
            int left = 2 * root + 1; // hijo izquierdo en árbol binario como arreglo
            int right = 2 * root + 2; // hijo derecho

            // verifica si algún hijo es mayor que la raíz actual
            if (left < size && arr[left].CompareTo(arr[largest]) > 0) largest = left;
            if (right < size && arr[right].CompareTo(arr[largest]) > 0) largest = right;

            // si la raíz no es el mayor, intercambia y sigue corrigiendo hacia abajo
            if (largest != root)
            {
                (arr[root], arr[largest]) = (arr[largest], arr[root]);
                Heapify(arr, size, largest); // propagación hacia abajo (sift-down)
            }
        }

        // Caso de uso: sistema de despacho de ambulancias donde los casos críticos
        // deben atenderse primero — HeapSort garantiza O(n log n) sin importar el input
        public static int[] UseCaseDemo()
        {
            // niveles de urgencia (1=bajo, 10=crítico)
            int[] urgencyLevels = { 3, 9, 1, 7, 5, 10, 2, 8 };
            Sort(urgencyLevels);
            return urgencyLevels; // → 1 2 3 5 7 8 9 10
        }
    }
}