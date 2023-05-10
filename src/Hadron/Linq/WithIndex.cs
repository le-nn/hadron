﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Linq;

public static class WithIndexExtension {
    public static IEnumerable<(T Value, int Index)> WithIndex<T>(this IEnumerable<T> sequence) {
        return sequence.Select((x, i) => (x, i));
    }
}