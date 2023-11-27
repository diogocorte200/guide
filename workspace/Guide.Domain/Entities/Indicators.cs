using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Domain.Entities
{

    public class Indicators
    {
        public List<Quote> Quote { get; set; }
        public List<AdjClose> Adjclose { get; set; }
    }
}
