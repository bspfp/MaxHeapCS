using System;

namespace MaxHeapCS {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Heap make / push / pop");
            var testHeap = new Heap<int>(new int[] { 5, 4, 3, 1, 2 });
            testHeap.Push(7);
            testHeap.Push(8);
            testHeap.Push(9);
            testHeap.Push(6);
            testHeap.Push(10);
            Console.Write("result:");
            while (testHeap.TryPop(out int v))
                Console.Write(" " + v);
            Console.WriteLine();

            Console.Write("Heap sort: source:");
            var testArray = new int[] { 5, 4, 3, 1, 2, 7, 8, 9, 6, 10 };
            foreach (var n in testArray)
                Console.Write(" " + n);
            Console.WriteLine();

            Heap<int>.Make(testArray);
            Console.Write("Heap sort: make:");
            foreach (var n in testArray)
                Console.Write(" " + n);
            Console.WriteLine();

            for (var i = testArray.Length; i > 1; i--)
                Heap<int>.Pop(testArray, i);
            Console.Write("Heap sort: result:");
            foreach (var n in testArray)
                Console.Write(" " + n);
            Console.WriteLine();
        }
    }
}
