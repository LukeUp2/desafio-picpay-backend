using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using picpay_simplificado.Data;
using picpay_simplificado.Models;

namespace picpay_simplificado.Repository
{
    public class WalletRepository
    {
        private readonly AppDbContext _context;
        public WalletRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task UpdateWallets(Transaction data)
        {
            var payerWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == data.Payer);
            var payeeWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserID == data.Payee);

            if (payerWallet == null || payeeWallet == null)
            {
                throw new Exception("Carteiras inválidas");
            }

            payerWallet.Amount -= data.Value;
            payeeWallet.Amount += data.Value;

            _context.Wallets.Update(payerWallet);
            _context.Wallets.Update(payeeWallet);
            await _context.SaveChangesAsync();
        }
    }
}