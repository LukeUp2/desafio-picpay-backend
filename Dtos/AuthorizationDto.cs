using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace picpay_simplificado.Dtos
{
    public class AuthorizationDto
    {
        public required string Status { get; set; }
        public required AuthotizationData Data { get; set; }
    }

    public class AuthotizationData
    {
        public bool Authorization { get; set; }
    }
}