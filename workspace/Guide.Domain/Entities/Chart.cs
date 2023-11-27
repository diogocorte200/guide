using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Domain.Entities
{

    public class Chart
    {
        public List<Result> Result { get; set; }
        public object Error { get; set; }
    }
}
