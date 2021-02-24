using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grasshopper.Kernel.Types;
using Grasshopper.Kernel.Data;

namespace SQLite_GH
{
    public static class Util
    {
        public static string concatenate(List<GH_String> values)
        {
            var concatenated = "(";
            for (int i = 0; i < values.Count; i++)
            {
                concatenated += "'";
                concatenated += values[i];
                concatenated += "'";

                if (i < values.Count - 1)
                    concatenated += ",";
            }
            concatenated += ")";
            return concatenated;
        }
    }
}
