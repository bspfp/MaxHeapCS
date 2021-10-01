using System;
using System.Collections.Generic;

namespace MaxHeapCS {
    class Heap<T> {
        public delegate bool Comparer(T a, T b);

        private const int DefaultCapacity = 8;

        private readonly List<T> data_;
        private readonly Comparer comparer_;

        public Heap(int capacity = DefaultCapacity) : this(DefaultComparer, capacity) { }
        public Heap(Comparer comparer, int capacity = DefaultCapacity) {
            data_ = new List<T>(capacity);
            comparer_ = comparer;
        }
        public Heap(IEnumerable<T> data) : this(DefaultComparer, data) { }
        public Heap(Comparer comparer, IEnumerable<T> data) {
            if (data == null) throw new ArgumentNullException();

            data_ = new List<T>(data);
            comparer_ = comparer;
            Make(data_, comparer_);
        }

        public void Push(T newElem) { Push(data_, newElem, comparer_); }
        public bool TryPeek(out T elem) {
            if (data_.Count > 0) {
                elem = data_[0];
                return true;
            }
            elem = default;
            return false;
        }
        public bool TryPop(out T elem) { return Pop(data_, out elem, comparer_); }
        public T Peek() {
            if (TryPeek(out T elem))
                return elem;
            if (Nullable.GetUnderlyingType(typeof(T)) != null || typeof(string).IsClass)
                return default;
            throw new InvalidOperationException();
        }
        public T Pop() {
            if (TryPop(out T elem))
                return elem;
            if (Nullable.GetUnderlyingType(typeof(T)) != null || typeof(string).IsClass)
                return default;
            throw new InvalidOperationException();
        }
        public void Clear() { data_.Clear(); }

        public static void Make(IList<T> data) { Make(data, DefaultComparer); }
        public static void Make(IList<T> data, Comparer comparer) {
            if (data == null) throw new ArgumentNullException();

            if (data.Count < 2)
                return;

            // 마지막 요소의 부모에서 시작
            for (var i = ParentOf(data.Count - 1); i >= 0; i--)
                Down(data, data.Count, comparer, i);
        }

        public static void Push(IList<T> data, T newElem) { Push(data, newElem, DefaultComparer); }
        public static void Push(IList<T> data, T newElem, Comparer comparer) {
            if (data == null) throw new ArgumentNullException();

            data.Add(newElem);
            Up(data, comparer, data.Count - 1);
        }

        public static bool Pop(IList<T> data, out T elem) { return Pop(data, out elem, DefaultComparer); }
        public static bool Pop(IList<T> data, out T elem, Comparer comparer) {
            if (data == null) throw new ArgumentNullException();

            if (!Pop(data, data.Count, comparer)) {
                elem = default;
                return false;
            }

            elem = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            return true;
        }
        public static bool Pop(IList<T> data, int count) { return Pop(data, count, DefaultComparer); }
        public static bool Pop(IList<T> data, int count, Comparer comparer) {
            if (data == null) throw new ArgumentNullException();
            if (data.Count < count) throw new ArgumentOutOfRangeException();

            if (count < 1)
                return false;
            if (count == 1)
                return true;
            Swap(data, 0, count - 1);
            Down(data, count - 1, comparer, 0);
            return true;
        }

        private static bool DefaultComparer(T a, T b) {
            if (a is IComparable a2)
                return a2.CompareTo(b) < 0;
            throw new ArgumentException("not IComparable");
        }

        private static int ParentOf(int n) { return (n - 1) / 2; }
        private static int LeftChildOf(int n) { return n * 2 + 1; }
        private static void Swap(IList<T> data, int n, int m) {
            T tmp = data[n];
            data[n] = data[m];
            data[m] = tmp;
        }

        private static void Down(IList<T> data, int count, Comparer comparer, int n) {
            while (true) {
                var leftChild = LeftChildOf(n);
                if (leftChild >= count)
                    break;

                var parent = n;
                var rightChild = leftChild + 1;

                // 자식 노드에서 큰 것 선택
                n = (rightChild < count && comparer(data[leftChild], data[rightChild])) ? rightChild : leftChild;

                if (comparer(data[parent], data[n]))
                    Swap(data, parent, n);
                else
                    break;
            }
        }

        private static void Up(IList<T> data, Comparer comparer, int n) {
            while (n > 0) {
                var parent = ParentOf(n);
                if (comparer(data[parent], data[n])) {
                    Swap(data, parent, n);
                    n = parent;
                }
                else
                    break;
            }
        }
    }
}
