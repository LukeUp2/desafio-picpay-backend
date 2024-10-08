using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using picpay_simplificado.Data;
using picpay_simplificado.Models;

namespace picpay_simplificado.Repository
{
    public class UserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User newUser)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    await _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();

                    Wallet newWallet = new Wallet() { Amount = 100.00m, UserID = newUser.Id };

                    await _context.Wallets.AddAsync(newWallet);
                    await _context.SaveChangesAsync();

                    newUser.Wallet = newWallet;

                    await transaction.CommitAsync();

                    return newUser;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<User?> GetById(int userId)
        {
            var user = await _context.Users.Include(u => u.Wallet).Where(u => u.Id == userId).SingleOrDefaultAsync();

            return user;
        }
    }
}