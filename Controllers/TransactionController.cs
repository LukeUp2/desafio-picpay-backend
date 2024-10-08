using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using picpay_simplificado.Dtos;
using picpay_simplificado.Services;

namespace picpay_simplificado.Controllers
{
    [ApiController]
    [Route("/api/transfer")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [HttpPost]
        public async Task<IActionResult> Transfer(TransferDto data)
        {
            try
            {
                var transfer = await _transactionService.Transfer(data);

                return Ok(transfer);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}