using System.Collections.Immutable;
using System.Runtime.CompilerServices;


namespace Hadron.Collections;

/// <summary>
/// Immutable array factories.
/// </summary>
public static class ImmutableArrayFactory {
    /// <summary>
    /// Initialize immutable array.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="inital">Initialize values.</param>
    /// <returns>The created immutable array instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<T> ArrayOf<T>(params T[]? inital) {
        return ImmutableArray.Create(inital);
    }

    /// <summary>
    /// Initialize immutable array.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="inital">Initialize values.</param>
    /// <returns>The created immutable array instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<T> ArrayOf<T>(T inital) {
        return ImmutableArray.Create(inital);
    }

    /// <summary>
    /// Initialize immutable array with IEnumerable{T}.
    /// </summary>
    /// <typeparam name="T">Item type.</typeparam>
    /// <param name="inital">Initialize values.</param>
    /// <returns>The created immutable array instance.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ImmutableArray<T> ArrayOfRange<T>(IEnumerable<T> inital) {
        return ImmutableArray.CreateRange(inital);
    }
}