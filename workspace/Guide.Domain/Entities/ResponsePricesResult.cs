using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Guide.Domain.Entities
{

    public class ResponsePricesResult
    {
        public int NumDia { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorAbertura { get; set; }
        public decimal? ComparacaoAoDiaAnterior { get; set; }
        public decimal? ComparacaoAoPrimeiroDia { get; set; }
    }
}