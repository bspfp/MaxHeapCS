using System;
using System.Collections.Generic;

namespace MaxHeapCS {
    public class Heap<T> {
        public Heap(int capacity = DefaultCapacity) : this(DefaultComparer, capacity) { }

        public Heap(Func<T, T, bool> comparer, int capacity = DefaultCapacity) {
            data = new List<T>(capacity);
            this.comparer = comparer;
        }

        public Heap(IEnumerable<T> data) : this(DefaultComparer, data) { }

        public Heap(Func<T, T, bool> comparer, IEnumerable<T> data) {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            this.data = new List<T>(data);
            this.comparer = comparer;

            if (this.data.Count > 1) {
                for (var i = ParentOf(this.data.Count - 1); i >= 0; i--)
                    Down(this.data.Count, i);
            }
        }

        public void Push(T newElem) {
            if (data == null)
                throw new InvalidDataException(nameof(data));

            data.Add(newElem);
            Up(data.Count - 1);
        }

        public bool TryPeek([MaybeNullWhen(false)] out T elem) {
            if (data.Count > 0) {
                elem = data[0];
                return true;
            }
            elem = default;
            return false;
        }

        public bool TryPop([MaybeNullWhen(false)] out T elem) {
            if (data == null)
                throw new InvalidDataException(nameof(data));

            if (data.Count < 1) {
                elem = default;
                return false;
            }

            if (data.Count == 1) {
                elem = data[0];
                data.Clear();
                return true;
            }

            Swap(0, data.Count - 1);
            Down(data.Count - 1, 0);

            elem = data[^1];
            data.RemoveAt(data.Count - 1);
            return true;
        }

        public void Clear() => data.Clear();

        private void Down(int count, int n) {
            while (true) {
                var leftChild = LeftChildOf(n);
                if (leftChild >= count)
                    break;

                var parent = n;
                var rightChild = leftChild + 1;

                n = (rightChild < count && comparer(data[leftChild], data[rightChild])) ? rightChild : leftChild;

                if (comparer(data[parent], data[n]))
                    Swap(parent, n);
                else
                    break;
            }
        }

        private void Up(int n) {
            while (n > 0) {
                var parent = ParentOf(n);
                if (comparer(data[parent], data[n])) {
                    Swap(parent, n);
                    n = parent;
                }
                else
                    break;
            }
        }

        private void Swap(int n, int m) => (data[m], data[n]) = (data[n], data[m]);

        private static int ParentOf(int n) => (n - 1) / 2;
        private static int LeftChildOf(int n) => n * 2 + 1;
        private static bool DefaultComparer(T a, T b) {
            if (a is IComparable a2)
                return a2.CompareTo(b) < 0;
            throw new ArgumentException("not IComparable");
        }

        private const int DefaultCapacity = 8;
        private readonly List<T> data;
        private readonly Func<T, T, bool> comparer;
    }
}
