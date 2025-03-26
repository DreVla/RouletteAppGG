using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouletteApp.Model
{
    public class RouletteResult
    {
        public int position { get; set; }
        public string color { get; set; }
        public int streak { get; set; }
        public int multiplier { get; set; }
    }
}
