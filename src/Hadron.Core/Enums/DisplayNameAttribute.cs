using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hadron.Enums;

public class DisplayNameAttribute : Attribute {
    public DisplayNameAttribute(string displayName) {
        DisplayName = displayName;
    }

    public string DisplayName { get; init; }
}