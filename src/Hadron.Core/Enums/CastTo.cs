using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Enums;
public static partial class EnumExtensions {
    public static TEnum CastTo<TEnum>(this Enum e, bool ignoreCase = true)
        where TEnum : struct, Enum =>
            Enum.TryParse<TEnum>(e.ToString(), ignoreCase, out var result)
                ? result
                : throw new InvalidCastException($"Cannot cast {e.GetType()} to {typeof(TEnum)}");
}
