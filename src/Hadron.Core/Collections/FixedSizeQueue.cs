using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Collections;

public class FixedSizeQueue<T> {
    public FixedSizeQueue(int size) {
        Queue = new Queue<T>(size);
        Size = size;
    }

    public int Size { get; private set; }

    public IReadOnlyCollection<T> Items => Queue.ToList();

    private System.Collections.Generic.Queue<T> Queue { get; }

    public void Enqueue(T item) {
        Queue.Enqueue(item);

        while (Queue.Count > Size) {
            Queue.TryDequeue(out _);
        }
    }

    public T Dequeue() {
        var result = Dequeue();
        return result;
    }
}