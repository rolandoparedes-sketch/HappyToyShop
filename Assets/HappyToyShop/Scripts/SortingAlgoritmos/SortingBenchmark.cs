using HappyToyShop.Collections.Sorting;
using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace HappyToyShop.Collections.Sorting.Demo
{
    // Benchmark visual que demuestra en consola de Unity por qué cada algoritmo
    // es mejor o peor según el tipo de datos y el tamaño del arreglo.
    public class SortingBenchmark : MonoBehaviour
    {
        [Title("Configuración")]
        [SerializeField, Range(100, 100000)] private int largeArraySize = 10000;
        [SerializeField, Range(5, 30)] private int smallArraySize = 16;
        [SerializeField] private int randomSeed = 42;

        // ────────────────────────────────────────────────────────────────
        //  BOTONES INDIVIDUALES — cada escenario muestra dónde brilla o falla
        // ────────────────────────────────────────────────────────────────

        [Title("Demos visuales (arreglo pequeño)")]
        [Button("Ver Bubble Sort paso a paso")]
        private void DemoBubble() => RunStepByStep("Bubble Sort", BubbleSort.Sort);

        [Button("Ver Selection Sort paso a paso")]
        private void DemoSelection() => RunStepByStep("Selection Sort", SelectionSort.Sort);

        [Button("Ver Insertion Sort paso a paso")]
        private void DemoInsertion() => RunStepByStep("Insertion Sort", InsertionSort.Sort);

        [Button("Ver Merge Sort paso a paso")]
        private void DemoMerge() => RunStepByStep("Merge Sort", MergeSort.Sort);

        [Button("Ver Quick Sort paso a paso")]
        private void DemoQuick() => RunStepByStep("Quick Sort", QuickSort.Sort);

        [Button("Ver Heap Sort paso a paso")]
        private void DemoHeap() => RunStepByStep("Heap Sort", HeapSort.Sort);

        // ────────────────────────────────────────────────────────────────
        //  BENCHMARK COMPLETO — timing en cada escenario
        // ────────────────────────────────────────────────────────────────

        [Title("Benchmark de rendimiento")]
        [Button("Correr TODOS los benchmarks", ButtonSizes.Large)]
        private void RunAllBenchmarks()
        {
            UnityEngine.Debug.Log(Header("BENCHMARK COMPLETO DE ALGORITMOS DE ORDENAMIENTO"));

            BenchmarkScenario("Arreglo ALEATORIO", GenerateRandom(largeArraySize));
            BenchmarkScenario("Arreglo CASI ORDENADO", GenerateNearlySorted(largeArraySize));
            BenchmarkScenario("Arreglo INVERSO", GenerateReverse(largeArraySize));
            BenchmarkScenario("Arreglo CON DUPLICADOS", GenerateDuplicates(largeArraySize));

            UnityEngine.Debug.Log(Footer("FIN DEL BENCHMARK"));
        }

        [Button("Demostrar casos mejores y peores")]
        private void RunBestWorstCases()
        {
            UnityEngine.Debug.Log(Header("CASOS MEJORES Y PEORES POR ALGORITMO"));

            ShowBestWorst("Bubble Sort", BubbleSort.Sort, "casi ordenado", "aleatorio grande");
            ShowBestWorst("Selection Sort", SelectionSort.Sort, "cualquier caso (siempre n-1 swaps)", "grande (n² comparaciones)");
            ShowBestWorst("Insertion Sort", InsertionSort.Sort, "casi ordenado / pequeño", "inverso grande");
            ShowBestWorst("Merge Sort", MergeSort.Sort, "cualquier caso (siempre n·log n)", "mucha memoria extra");
            ShowBestWorst("Quick Sort", QuickSort.Sort, "aleatorio grande", "ordenado sin pivote bueno");
            ShowBestWorst("Heap Sort", HeapSort.Sort, "garantía n·log n sin memoria extra", "caché miss frecuente");
        }

        // ────────────────────────────────────────────────────────────────
        //  LÓGICA INTERNA
        // ────────────────────────────────────────────────────────────────

        // Ejecuta un algoritmo sobre un arreglo pequeño y muestra el estado ANTES y DESPUÉS
        // con una barra visual tipo histograma en la consola
        private void RunStepByStep(string name, Action<int[]> sortFn)
        {
            int[] arr = GenerateRandom(smallArraySize, randomSeed);

            var sb = new StringBuilder();
            sb.AppendLine(Separator('─', 60));
            sb.AppendLine($"  {name.ToUpper()} — arreglo de {smallArraySize} elementos");
            sb.AppendLine(Separator('─', 60));

            sb.AppendLine("\nANTES de ordenar:");
            sb.AppendLine(DrawBars(arr));

            var sw = Stopwatch.StartNew();
            sortFn(arr);
            sw.Stop();

            sb.AppendLine($"\nDESPUÉS de ordenar ({sw.Elapsed.TotalMilliseconds:F4} ms):");
            sb.AppendLine(DrawBars(arr));
            sb.AppendLine(Separator('─', 60));

            UnityEngine.Debug.Log(sb.ToString());
        }

        // Mide el tiempo de todos los algoritmos sobre el mismo input y los muestra en tabla
        private void BenchmarkScenario(string scenarioName, int[] baseArray)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\n  ESCENARIO: {scenarioName}  (n = {baseArray.Length:N0})");
            sb.AppendLine(Separator('─', 56));
            sb.AppendLine($"  {"Algoritmo",-20} {"Tiempo (ms)",12}  {"Complejidad",14}");
            sb.AppendLine(Separator('─', 56));

            MeasureAndLog(sb, "Bubble Sort", BubbleSort.Sort, "O(n²)", baseArray);
            MeasureAndLog(sb, "Selection Sort", SelectionSort.Sort, "O(n²)", baseArray);
            MeasureAndLog(sb, "Insertion Sort", InsertionSort.Sort, "O(n²)", baseArray);
            MeasureAndLog(sb, "Merge Sort", MergeSort.Sort, "O(n·log n)", baseArray);
            MeasureAndLog(sb, "Quick Sort", QuickSort.Sort, "O(n·log n)†", baseArray);
            MeasureAndLog(sb, "Heap Sort", HeapSort.Sort, "O(n·log n)", baseArray);

            sb.AppendLine(Separator('─', 56));
            sb.AppendLine("  † Quick Sort: peor caso O(n²) sin pivote aleatorio\n");

            UnityEngine.Debug.Log(sb.ToString());
        }

        private void MeasureAndLog(StringBuilder sb, string name, Action<int[]> sortFn,
                                   string complexity, int[] baseArray)
        {
            int[] copy = (int[])baseArray.Clone();
            var sw = Stopwatch.StartNew();
            sortFn(copy);
            sw.Stop();

            double ms = sw.Elapsed.TotalMilliseconds;
            string bar = SpeedBar(ms, 3000.0); // 3000 ms = barra llena
            sb.AppendLine($"  {name,-20} {ms,10:F3} ms  {complexity,14}  {bar}");
        }

        private void ShowBestWorst(string name, Action<int[]> sortFn, string bestDesc, string worstDesc)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"\n  {name.ToUpper()}");
            sb.AppendLine($"  ✓ Mejor caso: {bestDesc}");
            sb.AppendLine($"  ✗ Peor caso:  {worstDesc}");

            // mide en arreglo casi ordenado (favorece Bubble e Insertion)
            int[] nearlySorted = GenerateNearlySorted(largeArraySize);
            double msNear = Measure(sortFn, nearlySorted);

            // mide en arreglo inverso (desfavorece Bubble, Insertion, Selection)
            int[] reversed = GenerateReverse(largeArraySize);
            double msRev = Measure(sortFn, reversed);

            sb.AppendLine($"  Tiempo casi ordenado: {msNear,8:F3} ms");
            sb.AppendLine($"  Tiempo inverso:       {msRev,8:F3} ms");
            sb.AppendLine($"  Ratio (inv/ord): x{msRev / Math.Max(msNear, 0.001):F1}");

            UnityEngine.Debug.Log(sb.ToString());
        }

        private double Measure(Action<int[]> sortFn, int[] baseArr)
        {
            int[] copy = (int[])baseArr.Clone();
            var sw = Stopwatch.StartNew();
            sortFn(copy);
            sw.Stop();
            return sw.Elapsed.TotalMilliseconds;
        }

        // ────────────────────────────────────────────────────────────────
        //  GENERADORES DE ARREGLOS DE PRUEBA
        // ────────────────────────────────────────────────────────────────

        private int[] GenerateRandom(int n, int seed = -1)
        {
            var rng = seed >= 0 ? new System.Random(seed) : new System.Random();
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rng.Next(1, n + 1);
            return arr;
        }

        // arreglo ordenado con ~5% de elementos en posición incorrecta
        private int[] GenerateNearlySorted(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = i + 1;

            var rng = new System.Random(randomSeed);
            int swaps = n / 20; // 5% de perturbación
            for (int k = 0; k < swaps; k++)
            {
                int i = rng.Next(n);
                int j = rng.Next(n);
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
            return arr;
        }

        private int[] GenerateReverse(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = n - i;
            return arr;
        }

        // arreglo donde solo hay k valores distintos (muchos duplicados)
        private int[] GenerateDuplicates(int n, int k = 10)
        {
            var rng = new System.Random(randomSeed);
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = rng.Next(1, k + 1);
            return arr;
        }

        // ────────────────────────────────────────────────────────────────
        //  HELPERS VISUALES
        // ────────────────────────────────────────────────────────────────

        // dibuja barras horizontales proporcionales al valor de cada elemento
        private string DrawBars(int[] arr)
        {
            int maxVal = 0;
            foreach (int v in arr) if (v > maxVal) maxVal = v;

            int maxBarLen = 40;
            var sb = new StringBuilder();

            foreach (int v in arr)
            {
                int barLen = maxVal > 0 ? (int)Math.Round((double)v / maxVal * maxBarLen) : 0;
                string bar = new string('█', barLen);
                sb.AppendLine($"  [{v,3}] {bar}");
            }
            return sb.ToString();
        }

        // barra de velocidad relativa: más llena = más lento
        private string SpeedBar(double ms, double maxMs)
        {
            int filled = (int)Math.Round(Math.Min(ms / maxMs, 1.0) * 10);
            return "[" + new string('■', filled) + new string('·', 10 - filled) + "]";
        }

        private string Separator(char c, int len) => "  " + new string(c, len);

        private string Header(string text)
        {
            string sep = new string('═', 60);
            return $"\n  {sep}\n  {text}\n  {sep}";
        }

        private string Footer(string text)
        {
            string sep = new string('═', 60);
            return $"  {sep}\n  {text}\n  {sep}\n";
        }
    }
}