using System;

namespace MaxHeapCS {
    class Program {
        static void Main(string[] args) {
            Heap<double> heap = new();
            for (int i = 0; i < 10; i++)
                heap.Push(Random.Shared.NextDouble());
            while(heap.TryPeek(out var elem)) {
                heap.TryPop(out var elem2);
                Console.WriteLine($"{elem}, {elem2}");
            }

            Console.WriteLine("press enter to exit...");
            Console.ReadLine();
            return 0;
        }
    }
}
