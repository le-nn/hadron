using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Enums;

public static partial class EnumExtensions {
    public static IEnumerable<(string Name, TEnum Value)> GetNameAndValues<TEnum>()
        where TEnum : struct, Enum {
        return Enum.GetNames<TEnum>().Zip(Enum.GetValues<TEnum>());
    }
}