using System.Collections;

namespace Hadron;

public static class RangedForeachExtension {
    /// <summary>
    /// Privides ranged foreach with <see cref="Range" />.
    /// </summary>
    /// <example>
    /// foreach(var i in 0..^1000)
    /// {
    /// }
    /// </example>
    /// <param name="range"></param>
    /// <returns></returns>
    public static RangeEnumerator GetEnumerator(this Range range) {
        return new(range);
    }
}

public struct RangeEnumerator : IEnumerator<int> {
    private readonly int Max;
    private readonly int Step;

    /// <summary>
    /// The current item position.
    /// </summary>
    public int Current { get; private set; }

    /// <summary>
    /// The current item.
    /// </summary>
    object IEnumerator.Current => Current;

    /// <summary>
    /// Move current cursor to next.
    /// </summary>
    /// <returns></returns>
    public bool MoveNext() {
        if (Current != Max) {
            Current += Step;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Dispose this.
    /// </summary>
    public void Dispose() {
        // noop
    }

    /// <summary>
    /// Reset cursor position.
    /// </summary>
    /// <_exception cref="NotSupportedException"></_exception>
    public void Reset() {
        throw new NotSupportedException();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="RangeEnumerator" /> struct.
    /// </summary>
    /// <param name="range">Initialize range.</param>
    public RangeEnumerator(Range range) {
        var step = range.End.Value < range.Start.Value ? -1 : 1;
        Current = range.Start.Value - (range.Start.IsFromEnd ? 0 : step);
        Max = range.End.Value - (range.End.IsFromEnd ? step : 0);
        Step = step;
    }
}