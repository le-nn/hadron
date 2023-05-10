﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Text;
public static partial class RepeatExtension {
    /// <summary>
    /// Repeat string.
    /// </summary>
    /// <param name="str">The target string.</param>
    /// <param name="repeat"></param>
    /// <returns>The value.</returns>
    public static string Repeat(this string str, int repeat) {
        var builder = new StringBuilder();

        for (var i = 0; i < repeat; i++) {
            builder.Append(str);
        }

        return builder.ToString();
    }
}
