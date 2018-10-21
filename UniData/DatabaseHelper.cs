using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniData
{
    public static class DatabaseHelper
    {
        public enum Input // Specifies if intent is to input rows or columns
        {
            Columns,
            Rows
        }
    }
}
