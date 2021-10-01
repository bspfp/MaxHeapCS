# MaxHeapCS

.Net Core 3.1을 위해 작성한 Heap 클래스입니다.

기본 동작은 max heap입니다.

내부에서 List<T>를 사용합니다.

### 예시

```c#
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

// Heap make / push / pop
// result: 10 9 8 7 6 5 4 3 2 1
// Heap sort: source: 5 4 3 1 2 7 8 9 6 10
// Heap sort: make: 10 9 8 6 4 7 3 1 5 2
// Heap sort: result: 1 2 3 4 5 6 7 8 9 10
```

### class Heap<T>

##### 생성자

- Heap(int capacity = DefaultCapacity)
  Heap(Comparer comparer, int capacity = DefaultCapacity)
  Heap(IEnumerable<T> data)
  Heap(Comparer comparer, IEnumerable<T> data)
  - int capacity
    heap 데이터를 위한 컨테이너의 공간을 지정합니다.
  - Comparer comparer
    T 타입 값 2개를 비교하는 방법을 지정합니다.
    C++의 STL과 같이 작은 값을 반환하면 max heap이 구성됩니다. (기본값)
  - IEnumerable<T> data
    data로 heap의 데이터를 초기 구성합니다.

##### 메서드

- void Push(T newElem)
  newElem을 Heap에 넣기
- bool TryPeek(out T elem)
  최상위 값을 읽기, heap에서 제거하지 않음
- bool TryPop(out T elem)
  최상위 값을 꺼내기, heap에서 제거됨
- T Peek()
  최상위 값을 읽기, heap에서 제거하지 않음
  데이터가 없을 때, T가 nullable이거나 class 이면 null을 반환하고, null이 불가능한 데이터 타입이면 InvalidOperationException 예외를 발생
- T Pop()
  최상위 값을 꺼내기, heap에서 제거됨
  데이터가 없을 때, T가 nullable이거나 class 이면 null을 반환하고, null이 불가능한 데이터 타입이면 InvalidOperationException 예외를 발생

##### 정적 메서드

- static void Make(IList<T> data)
  static void Make(IList<T> data, Comparer comparer)
  data를 heap으로 구성합니다.
- static void Push(IList<T> data, T newElem)
  static void Push(IList<T> data, T newElem, Comparer comparer)
  newElem을 data에 추가하여 heap을 구성합니다. data는 이미 heap으로 구성되어 있어야 합니다.
- static bool Pop(IList<T> data, out T elem)
  static bool Pop(IList<T> data, out T elem, Comparer comparer)
  static bool Pop(IList<T> data, int count)
  static bool Pop(IList<T> data, int count, Comparer comparer)
  data에서 최상위 값을 꺼냅니다.
  count는 data의 길이를 지정합니다. (예시의 heap sort 참고) 
