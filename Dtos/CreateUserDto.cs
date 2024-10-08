using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using picpay_simplificado.Enums;
using picpay_simplificado.Models;

namespace picpay_simplificado.Dtos
{
    public class CreateUserDto
    {
        public required string Name { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public UserEnum Type { get; set; }
        public required string Password { get; set; }
        public Wallet? Wallet { get; set; }
    }
}