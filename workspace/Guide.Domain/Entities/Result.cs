using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Domain.Entities
{
    public class Result
    {
        public List<int> Timestamp { get; set; }
        public Indicators Indicators { get; set; }
    }
}
