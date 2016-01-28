using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CnC.Helpers
{
    public static class CSVParser
    {

        public static Array[] getValues(string fileName, int headLinesToSkip, int columnsToSkip)
        {
            fileName = AppDomain.CurrentDomain.BaseDirectory + fileName;
            var lines = File.ReadAllLines(fileName).Select(a => a.Split(','));
            var csv = (from line in lines
                       select (from col in line
                               select col).Skip(columnsToSkip).ToArray() // skip column
                      ).Skip(headLinesToSkip).ToArray(); // skip  headlines
            
            return csv;
        }


    }
}
