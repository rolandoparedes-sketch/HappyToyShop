using System;

namespace HappyToyShop.Collections.Sorting
{
    // BUBBLE SORT — "Ordenamiento burbuja"
    // Nombre: en cada pasada, los valores mayores "burbujean" (flotan) hacia el final del arreglo,
    // igual que las burbujas de aire suben al tope del agua.
    //
    // Cuándo usarlo:
    //   ✓ Arreglos casi ordenados (la bandera swapped lo convierte en O(n))
    //   ✓ Fines educativos / debugging visual paso a paso
    //   ✗ Arreglos grandes o completamente desordenados
    //
    // Mejor: O(n) | Promedio: O(n²) | Peor: O(n²) | Espacio: O(1) | Estable: Sí
    public static class BubbleSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable<T>
        {
            int n = arr.Length;

            for (int i = 0; i < n - 1; i++)
            {
                bool swapped = false;

                // después de cada pasada, los últimos i elementos ya están en su lugar final
                for (int j = 0; j < n - 1 - i; j++)
                {
                    if (arr[j].CompareTo(arr[j + 1]) > 0)
                    {
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                        swapped = true;
                    }
                }

                // optimización clave: si no hubo ningún swap, el arreglo ya está ordenado
                if (!swapped) break;
            }
        }

        // Caso de uso: puntajes de un partido casi ordenados, solo un jugador cambió de posición
        public static int[] UseCaseDemo()
        {
            // casi ordenado → Bubble termina en 1-2 pasadas gracias a la bandera
            int[] scores = { 91, 92, 93, 95, 89, 98 };
            Sort(scores);
            return scores; // → 89 91 92 93 95 98
        }
    }
}