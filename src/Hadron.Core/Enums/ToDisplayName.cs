using System.Reflection;


namespace Hadron.Enums;

public static partial class EnumExtensions {
    public static string? ToDisplayName(this Enum e) {
        return e.GetType().GetField(e.ToString())?.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName ?? "";
    }
}