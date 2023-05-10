using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Tasks;

public static partial class TaskExtension {
    public static Task<T> AsTask<T>(this T obj) {
        return Task.FromResult(obj);
    }
}