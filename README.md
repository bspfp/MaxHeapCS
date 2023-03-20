# MaxHeapCS

.Net 7에서 작성한 Heap 클래스입니다.

기본 동작은 max heap입니다.

내부에서 List&lt;T&gt;를 사용합니다.

### 예시

```c#
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
}
```
