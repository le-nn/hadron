namespace Hadron;

/// <summary>
/// Represents pipeline operetor with async operation.
/// </summary>
public class Pipeline {
    internal Pipeline() { }

    public static Pipeline<U> Create<U>(U value) {
        return new Pipeline<U>(() => value);
    }

    public static Pipeline<U> Create<U>(Func<U> func) {
        return new Pipeline<U>(() => func());
    }

    public static AsyncPipeline<U> Create<U>(Func<Task<U>> func) {
        return new AsyncPipeline<U>(() => func());
    }

    public static Pipeline Create() {
        return new();
    }

    public Pipeline<T> Pipe<T>(Func<T> func) {
        return new Pipeline<T>(() => func());
    }
}

/// <summary>
/// Represents pipeline operetor with async operation.
/// </summary>
/// <typeparam name="T">The value that you want to pipe.</typeparam>
public class Pipeline<T> : Pipeline {
    /// <summary>
    /// Initializes a new instance of the <see cref="Pipeline{T}" /> class.
    /// </summary>
    /// <param name="func">The value selector.</param>
    internal Pipeline(Func<T> func) {
        Func = func;
    }

    /// <summary>
    /// The value selector.
    /// </summary>
    protected Func<T> Func { get; }

    /// <summary>
    /// Connects and convolves a series of functions and return values
    /// </summary>
    /// <typeparam name="U">Argument type</typeparam>
    /// <param name="func">To pipe function</param>
    /// <returns>The connected pipe instance.</returns>
    public Pipeline<U> Pipe<U>(Func<T, U> func) {
        return new Pipeline<U>(() => func(Func()));
    }

    /// <summary>
    /// Innterupt action.
    /// </summary>
    /// <typeparam name="T">Argument type</typeparam>
    /// <param name="func">To innterupt async action</param>
    /// <returns>The connected pipe.</returns>
    public Pipeline<T> Action(Action<T> func) {
        return new Pipeline<T>(() => {
            var result = Func();
            func(result);
            return result;
        });
    }

    /// <summary>
    /// Connects and convolves a series of functions and return values. Connect to async pipe.
    /// </summary>
    /// <typeparam name="U">Argument type</typeparam>
    /// <param name="func">To pipe function</param>
    /// <returns>The connected async pipe.</returns>
    public AsyncPipeline<U> PipeAsync<U>(Func<T, Task<U>> func) {
        return new AsyncPipeline<U>(() => func(Func()));
    }

    /// <summary>
    /// Execute all piped actions.
    /// </summary>
    /// <returns>The piped result.</returns>
    public T Execute() {
        return Func();
    }
}

/// <summary>
/// Represents pipeline operetor.
/// </summary>
/// <typeparam name="T">The value that you want to pipe.</typeparam>
public class AsyncPipeline<T> : Pipeline {
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncPipeline{T}" /> class.
    /// </summary>
    /// <param name="func">The value selector.</param>
    internal AsyncPipeline(Func<Task<T>> func) {
        Func = func;
    }

    /// <summary>
    /// The value selector.
    /// </summary>
    protected Func<Task<T>> Func { get; }

    /// <summary>
    /// Connects and convolves a series of functions and return values.
    /// </summary>
    /// <typeparam name="U">Argument type</typeparam>
    /// <param name="func">To pipe function</param>
    /// <returns></returns>
    public AsyncPipeline<U> PipeAsync<U>(Func<T, Task<U>> func) {
        return new AsyncPipeline<U>(async () => await func(await Func()));
    }

    /// <summary>
    /// Connects and convolves a series of functions and return values.
    /// </summary>
    /// <typeparam name="U">Argument type</typeparam>
    /// <param name="func">To pipe function</param>
    /// <returns></returns>
    public AsyncPipeline<U> PipeAsync<U>(Func<T, U> func) {
        return new AsyncPipeline<U>(async () => func(await Func()));
    }

    /// <summary>
    /// Innterupt action.
    /// </summary>
    /// <typeparam name="T">Argument type</typeparam>
    /// <param name="func">To innterupt async action</param>
    /// <returns></returns>
    public AsyncPipeline<T> ActionAsync(Func<T, Task> func) {
        return new AsyncPipeline<T>(async () => {
            var result = await Func();
            await func(result);
            return result;
        });
    }

    /// <summary>
    /// Innterupt action.
    /// </summary>
    /// <typeparam name="T">Argument type</typeparam>
    /// <param name="func">To innterupt async action</param>
    /// <returns></returns>
    public AsyncPipeline<T> ActionAsync(Action<T> func) {
        return new AsyncPipeline<T>(async () => {
            var result = await Func();
            func(result);
            return result;
        });
    }

    /// <summary>
    /// Execute all piped actions async.
    /// </summary>
    /// <returns></returns>
    public async Task<T> ExecuteAsync() {
        return await Func();
    }
}