using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Domain.Entities
{
    public class Quote
    {
        public List<double> Low { get; set; }
        public List<decimal> Open { get; set; }
        public List<double> Close { get; set; }
        public List<double> High { get; set; }
        public List<int> Volume { get; set; }
    }
}
