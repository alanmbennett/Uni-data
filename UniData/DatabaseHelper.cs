using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Class: DatabaseHelper
 * Description: Static class used to store helpful functionality and/or variables in regards to the database
 */

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
