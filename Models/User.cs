using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using picpay_simplificado.Enums;

namespace picpay_simplificado.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public UserEnum Type { get; set; } = UserEnum.RergularUser;
        public string Password { get; set; } = "";
        public Wallet? Wallet { get; set; }
    }
}