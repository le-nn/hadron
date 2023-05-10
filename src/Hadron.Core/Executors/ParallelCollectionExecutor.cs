using System.Collections.Concurrent;

namespace Hadron.Executors;

public class ParallelCollectionExecutor<T>(ICollection<T> tasks, int parallelCount = 100) {
    private readonly ConcurrentQueue<T> _tasks = new(tasks);
    private readonly int _parallelCount = parallelCount;
    private int _progress;
    private bool _isInvoked = false;

    public int Progress => _progress;

    public int Count => _tasks.Count;

    public TimeSpan DelayPerStartup { get; set; } = TimeSpan.FromMilliseconds(100);

    public async Task RunAsync(Func<T, Task> handler) {
        if (_isInvoked) {
            throw new InvalidOperationException("This handler is already invoked.");
        }

        _isInvoked = true;
        await Task.WhenAll(Run(handler));
    }

    private IEnumerable<Task> Run(Func<T, Task> handler) {
        foreach (var i in 0..^_parallelCount) {
            yield return Task.Run(async () => {
                await Task.Delay(DelayPerStartup.Milliseconds * i);
                while (_tasks.TryDequeue(out var t)) {
                    Interlocked.Increment(ref _progress);
                    await handler(t);
                }
            });
        }
    }
}