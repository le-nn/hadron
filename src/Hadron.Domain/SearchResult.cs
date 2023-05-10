namespace Pictagram.Models.Helpers;

public record SearchResult<T>(
    int Total,
    int Offset,
    int Fetch,
    ImmutableArray<T> Items
);
