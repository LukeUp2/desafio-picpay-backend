using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using picpay_simplificado.Dtos;
using picpay_simplificado.Enums;
using picpay_simplificado.Models;
using picpay_simplificado.Repository;

namespace picpay_simplificado.Services
{
    public class TransactionService
    {
        private readonly UserRepository _userRepository;
        private readonly UserService _userService;
        private readonly TransactionRepository _transactionRepository;
        public TransactionService(UserRepository userRepository, TransactionRepository transactionRepository, UserService userService)
        {
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _userService = userService;
        }
        public async Task<Transaction> Transfer(TransferDto transferDto)
        {
            var payer = await _userRepository.GetById(transferDto.Payer);
            if (payer == null || payer.Type == UserEnum.Merchant)
            {
                throw new Exception("Pagador não encontrado ou inválido");
            }

            if (payer.Wallet == null || payer.Wallet.Amount < transferDto.Value)
            {
                throw new Exception("Saldo Insuficiente");
            }

            var authorized = await CheckAuthorization();
            if (authorized == false)
            {
                throw new Exception("Não Autorizado");
            }

            Transaction transaction = new()
            {
                Value = transferDto.Value,
                Payer = transferDto.Payer,
                Payee = transferDto.Payee,
            };

            var sucessTransaction = await _transactionRepository.Execute(transaction);

            await _userService.NotifyUser(sucessTransaction.Payee);

            return sucessTransaction;

        }

        private async Task<bool> CheckAuthorization()
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    string url = "https://util.devi.tools/api/v2/authorize";

                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    var responseBody = await response.Content.ReadAsStringAsync();

                    AuthorizationDto? apiData = JsonConvert.DeserializeObject<AuthorizationDto>(responseBody);

                    if (apiData == null) return false;

                    return apiData.Data.Authorization;
                }
                catch (HttpRequestException)
                {
                    throw;
                }
            }
        }
    }
}