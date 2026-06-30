using System;

namespace HappyToyShop.Collections.Sorting
{
    // INSERTION SORT — "Ordenamiento por inserción"
    // Nombre: funciona como ordenar cartas en la mano — tomas una carta y la INSERTAS
    // en la posición correcta dentro de las cartas ya ordenadas a tu izquierda.
    //
    // Cuándo usarlo:
    //   ✓ Arreglos pequeños (n < 20): supera a QuickSort por su baja constante
    //   ✓ Arreglos casi ordenados: O(n) cuando hay pocos elementos fuera de lugar
    //   ✓ Ordenamiento en línea (online): puede ordenar datos que llegan uno a uno
    //   ✗ Arreglos grandes y desordenados
    //
    // Mejor: O(n) | Promedio: O(n²) | Peor: O(n²) | Espacio: O(1) | Estable: Sí
    public static class InsertionSort
    {
        public static void Sort<T>(T[] arr) where T : IComparable<T>
        {
            int n = arr.Length;

            for (int i = 1; i < n; i++)
            {
                T key = arr[i]; // elemento que vamos a insertar en su posición correcta
                int j = i - 1;

                // desplaza hacia la derecha todos los elementos mayores que key
                while (j >= 0 && arr[j].CompareTo(key) > 0)
                {
                    arr[j + 1] = arr[j];
                    j--;
                }

                arr[j + 1] = key; // inserta key en su hueco correcto
            }
        }

        // Caso de uso: nuevo jugador entra a la tabla de líderes ya ordenada
        // Solo necesita "insertarse" en su posición, el resto permanece igual
        public static int[] UseCaseDemo()
        {
            // tabla de líderes ya casi ordenada, llegan nuevos puntajes uno a uno
            int[] leaderboard = { 100, 95, 90, 85, 72, 80 }; // 80 está fuera de lugar
            Sort(leaderboard);
            return leaderboard; // → 72 80 85 90 95 100
        }
    }
}