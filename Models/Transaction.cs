using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picpay_simplificado.Models
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        //Pagador
        public int Payer { get; set; }
        //Beneficiario
        public int Payee { get; set; }
        public string CreatedAt { get; set; } = DateTime.Now.ToString();
    }
}