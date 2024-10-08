using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picpay_simplificado.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int UserID { get; set; }
    }
}