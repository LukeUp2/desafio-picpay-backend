using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using picpay_simplificado.Data;
using picpay_simplificado.Dtos;
using picpay_simplificado.Models;

namespace picpay_simplificado.Repository
{
    public class TransactionRepository
    {
        private readonly AppDbContext _context;
        private readonly WalletRepository _walletRepository;
        public TransactionRepository(AppDbContext context, WalletRepository walletRepository)
        {
            _context = context;
            _walletRepository = walletRepository;
        }

        public async Task<Transaction> Execute(Transaction data)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Transactions.AddAsync(data);
                await _context.SaveChangesAsync();

                await _walletRepository.UpdateWallets(data);

                await transaction.CommitAsync();

                return data;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}