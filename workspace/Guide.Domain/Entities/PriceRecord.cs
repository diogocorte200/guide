using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Domain.Entities
{
    public class PriceRecord
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal OpenPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
