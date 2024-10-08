using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picpay_simplificado.Dtos
{
    public class TransferDto
    {
        public decimal Value { get; set; }
        public int Payer { get; set; }
        public int Payee { get; set; }
    }
}