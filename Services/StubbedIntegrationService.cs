

using System;
using System.Linq;
using System.Threading.Tasks;
using Dragon.Common.Contracts.Enums;
using Dragon.Common.Contracts.Integration.Classes;
using IntegrationSample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dragon.Services
{
    public interface IStubbedIntegrationService
    {
        Task<StartGameResponse> StartGame(StartGameRequest request);
        Task<BalanceResponse> Balance(BalanceRequest request);
        Task<DebitResponse> Debit(DebitRequest request);
        Task<CreditResponse> Credit(CreditRequest request);
        Task<CancelRoundResponse> CancelRound(CancelRoundRequest request);
        Task<CloseGameSessionResponse> CloseGameSession(CloseGameSessionRequest request);
        Task<DebitCreditResponse> DebitCredit(DebitCreditRequest request);
    }
    public class StubbedIntegrationService : IStubbedIntegrationService
    {
        private readonly ILogger<StubbedIntegrationService> _logger;
        private readonly MyAppDbContext _db;

        private readonly Random rnd = new Random();

        public StubbedIntegrationService(MyAppDbContext db, ILogger<StubbedIntegrationService> logger)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<BalanceResponse> Balance(BalanceRequest request)
        {


            // We get the player based on the external id. the external id is the id you send ind the start game response.
            var player = await _db.Players.FirstOrDefaultAsync(a => a.Id == Convert.ToInt64(request.ExternalPlayerId));

            // if no player found we have an invalid external id, this would be critical error or indicate that players has been removed.
            if (player == null)
                return new BalanceResponse() { Code = IntegrationResponseCode.InvalidPlayerId };

            // if player locked, respond with player locked.
            if (player.Locked)
                return new BalanceResponse() { Code = IntegrationResponseCode.PlayerLocked };


            var response = new BalanceResponse()
            {
                Currency = player.Currency,
                Balance = player.Balance,
                BonusBalance = player.BonusBalance,
            };

            // Note, we do not set Code or Message, by default this will be "OK" values.
            return response;
        }
        public async Task<CancelRoundResponse> CancelRound(CancelRoundRequest request)
        {

            // We get the player based on the external id. the external id is the id you send ind the start game response.
            var player = await _db.Players.FirstOrDefaultAsync(a => a.Id == Convert.ToInt64(request.ExternalPlayerId));

            // if no player found we have an invalid external id, this would be critical error or indicate that players has been removed.
            if (player == null)
                return new CancelRoundResponse() { Code = IntegrationResponseCode.InvalidPlayerId };

            // get all transactions that are not cancelled for that round.
            var transactions = await _db.Transactions.Where(a => a.RoundId == request.RoundId && a.Status != TransactionStatus.Cancelled).ToListAsync();
            foreach (var t in transactions)
            {
                // Cancel transaction
                t.Status = TransactionStatus.Cancelled;

                // Add amount back to players balance
                player.Balance += t.DebitAmount;
                player.Balance -= t.CreditAmount;

                _logger.LogInformation("Cancelled round : " + t.RoundId + " returned : " + t.Amount);

                await _db.SaveChangesAsync();
            }


            return new CancelRoundResponse() { Code = IntegrationResponseCode.Ok };
        }
        public async Task<CloseGameSessionResponse> CloseGameSession(CloseGameSessionRequest request)
        {
            var response = new CloseGameSessionResponse() { };

            return await Task.FromResult(response);
        }

        public async Task<CreditResponse> Credit(CreditRequest request)
        {
            System.Threading.Thread.Sleep(50);
            //Timeout hack, 1 in 10
            //      if (rnd.Next(9) == 0)
            //        System.Threading.Thread.Sleep(26000);

            if (request.BuyIn && request.NrOfRounds == 0)
                return new CreditResponse { Code = IntegrationResponseCode.Error, Message = "resolver test error" };

            // We get the player based on the external id. the external id is the id you send ind the start game response.
            var player = await _db.Players.FirstOrDefaultAsync(a => a.Id == Convert.ToInt64(request.ExternalPlayerId));

            // if no player found we have an invalid external id, this would be critical error or indicate that players has been removed.
            if (player == null)
                return new CreditResponse() { Code = IntegrationResponseCode.InvalidPlayerId };

            // First we check if this transaction has been made before. if its a retrigger. if so its important we return same information without creating a new one or make a new deduction.
            var transaction = await _db.Transactions.FirstOrDefaultAsync(a => a.ExternalTransactionId == request.TransactionId);
            if (transaction != null)
            {
                return new CreditResponse()
                {
                    ExternalTransactionId = transaction.Id.ToString(),
                    Currency = player.Currency,
                    Balance = player.Balance,
                    BonusBalance = player.BonusBalance,
                };
            }


            // if player locked, respond with player locked.
            if (player.Locked)
                return new CreditResponse() { Code = IntegrationResponseCode.PlayerLocked };

            // Check player balance  WHY THE FUCKL WAS THIS SHERE? most have been left from copy and paste.... makes no sense.
            //     if ((player.BonusBalance + player.Balance) < request.Amount)
            //       return new CreditResponse() { Code = IntegrationResponseCode.InsufficientBalance };

            //Create the transaction
            _db.Transactions.Add(new Transaction()
            {
                ExternalTransactionId = request.TransactionId,
                PlayerId = player.Id,
                Type = TransactionType.Credit,
                CreditAmount = request.Amount,
                DebitAmount = 0,
                Amount = request.Amount,
                Time = DateTime.UtcNow,
                RoundId = request.RoundId,
                Status = TransactionStatus.Completed
            });


            // Add amount to players balance.
            player.Balance += request.Amount;


            _logger.LogDebug(player.Id + " new Balance: " + player.Balance);


            await _db.SaveChangesAsync();

            var response = new CreditResponse()
            {
                Currency = player.Currency,
                Balance = player.Balance,
                BonusBalance = player.BonusBalance,
                ExternalTransactionId = null // this needs to be unique if you wish to provide it.
            };

            return response;
        }

        public async Task<DebitResponse> Debit(DebitRequest request)
        {
            System.Threading.Thread.Sleep(50);

            //Timeout hack, 1 in 10
            //    if (rnd.Next(9) == 0)
            //        System.Threading.Thread.Sleep(26000);


            // We get the player based on the external id. the external id is the id you send ind the start game response.
            var player = await _db.Players.FirstOrDefaultAsync(a => a.Id == Convert.ToInt64(request.ExternalPlayerId));

            // if no player found we have an invalid external id, this would be critical error or indicate that players has been removed.
            if (player == null)
                return new DebitResponse() { Code = IntegrationResponseCode.InvalidPlayerId };

            // First we check if this transaction has been made before. if its a retrigger. if so its important we return same information without creating a new one or make a new deduction.
            var transaction = await _db.Transactions.FirstOrDefaultAsync(a => a.ExternalTransactionId == request.TransactionId);
            if (transaction != null)
            {
                return new DebitResponse()
                {
                    ExternalTransactionId = transaction.ExternalTransactionId.ToString(),
                    Currency = player.Currency,
                    Balance = player.Balance,
                    BonusBalance = player.BonusBalance,
                };
            }

            // if player locked, respond with player locked.
            if (player.Locked)
                return new DebitResponse() { Code = IntegrationResponseCode.PlayerLocked };

            // Check player balance
            if ((player.BonusBalance + player.Balance) < request.Amount)
                return new DebitResponse() { Code = IntegrationResponseCode.InsufficientBalance };


            //Create the transaction.
            _db.Transactions.Add(new Transaction()
            {
                ExternalTransactionId = request.TransactionId,
                PlayerId = player.Id,
                Type = TransactionType.Debit,
                CreditAmount = 0,
                DebitAmount = request.Amount,
                Amount = request.Amount,
                Time = DateTime.UtcNow,
                RoundId = request.RoundId,
                Status = TransactionStatus.Completed
            });


            // Dirty way, this is not at all recommended way of doing it, just for sample, not safe.
            if (player.BonusBalance < request.Amount)
            {
                var left = (request.Amount - player.BonusBalance);
                player.BonusBalance = 0;

                player.Balance -= left;
            }
            else
            {
                player.BonusBalance -= request.Amount;
            }

            await _db.SaveChangesAsync();
            _logger.LogDebug(player.Id + " new Balance: " + player.Balance);
            var response = new DebitResponse()
            {
                Currency = player.Currency,
                Balance = player.Balance,
                BonusBalance = player.BonusBalance,
                ExternalTransactionId = null // this needs to be unique if you wish to provide it.
            };


            return response;
        }

        public async Task<DebitCreditResponse> DebitCredit(DebitCreditRequest request)
        {
            System.Threading.Thread.Sleep(50);
            //Timeout hack, 1 in 10
            //if (rnd.Next(9) == 0)
            //    System.Threading.Thread.Sleep(26000);

            // We get the player based on the external id. the external id is the id you send ind the start game response.
            var player = await _db.Players.FirstOrDefaultAsync(a => a.Id == Convert.ToInt64(request.ExternalPlayerId));

            // if no player found we have an invalid external id, this would be critical error or indicate that players has been removed.
            if (player == null)
                return new DebitCreditResponse() { Code = IntegrationResponseCode.InvalidPlayerId };

            // First we check if this transaction has been made before. if its a retrigger. if so its important we return same information without creating a new one or make a new deduction.
            var transaction = await _db.Transactions.FirstOrDefaultAsync(a => a.ExternalTransactionId == request.TransactionId);
            if (transaction != null)
            {
                return new DebitCreditResponse()
                {
                    ExternalTransactionId = transaction.ExternalTransactionId.ToString(),
                    Currency = player.Currency,
                    Balance = player.Balance,
                    BonusBalance = player.BonusBalance,
                };
            }

            // if player locked, respond with player locked.
            if (player.Locked)
                return new DebitCreditResponse() { Code = IntegrationResponseCode.PlayerLocked };


            // IMPORTANT! WARNING! note this is before any transactions is done.
            // Check player balance, its imporant we check the DebitAmount to ensure the player is not betting money he does not have and win.
            if ((player.BonusBalance + player.Balance) < request.DebitAmount)
                return new DebitCreditResponse() { Code = IntegrationResponseCode.InsufficientBalance };


            // Deduct/add balance.
            // Dirty way, this is not at all recommended way of doing it, just for sample, not safe.
            if (player.BonusBalance < request.DebitAmount)
            {
                var left = (request.DebitAmount - player.BonusBalance);
                player.BonusBalance = 0;

                player.Balance -= left;
            }
            else
            {
                player.BonusBalance -= request.DebitAmount;
            }

            // Add amount to players balance.
            player.Balance += request.CreditAmount;


            //Create the transaction. Note we record only the sum of it, and set it as debit or credit depending on if the player gained or lost
            // This can be done in other ways, but NOTE! we will only send 1 transcation id for Debitcredit request.
            _db.Transactions.Add(new Transaction()
            {
                ExternalTransactionId = request.TransactionId,
                PlayerId = player.Id,
                Type = request.Amount < 0 ? TransactionType.Debit : TransactionType.Credit,
                DebitAmount = request.DebitAmount,
                CreditAmount = request.CreditAmount,
                Amount = request.Amount,
                Time = DateTime.UtcNow,
                RoundId = request.RoundId,
                Status = TransactionStatus.Completed

            });

            await _db.SaveChangesAsync();

            var response = new DebitCreditResponse()
            {
                Currency = player.Currency,
                Balance = player.Balance,
                BonusBalance = player.BonusBalance,
                ExternalTransactionId = null // this needs to be unique if you wish to provide it.
            };

            _logger.LogDebug(player.Id + " new Balance: " + player.Balance);
            return response;
        }

        public async Task<StartGameResponse> StartGame(StartGameRequest request)
        {


            // We get the player based on the token.
            // NOTE! Token should be generated at every game start on the operator side. Do not make it constant or re use.
            var player = await _db.Players.FirstOrDefaultAsync(a => a.Token == request.Token);

            if (player == null && request.Token.Contains("random-"))
            {
                await _db.Players.AddAsync(new Player()
                {
                    Id = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds,
                    Brand = "myexamplebrand",
                    Nickname = request.Token,
                    Country = "SE",
                    Language = "en_GB",
                    Currency = "EUR",
                    Registered = DateTime.Parse("2019-01-01"),
                    Birthdate = DateTime.Parse("1980-01-01"),
                    Locked = false,
                    Balance = 1000M,
                    BonusBalance = 0,
                    Token = request.Token

                });
                await _db.SaveChangesAsync();
                player = await _db.Players.FirstOrDefaultAsync(a => a.Token == request.Token);

            }
            // if no player found it was likely an invalid token.
            else if (player == null)
            {
                _logger.LogWarning("Invalid Token");
                return new StartGameResponse() { Code = IntegrationResponseCode.InvalidToken };
            }

            // if player locked, respond with player locked.
            if (player.Locked)
                return new StartGameResponse() { Code = IntegrationResponseCode.PlayerLocked };


            var response = new StartGameResponse()
            {
                ExternalPlayerId = player.Id.ToString(),
                Nickname = player.Nickname,
                Currency = player.Currency,
                Brand = player.Brand,
                Language = player.Language,
                Balance = player.Balance,
                BonusBalance = player.BonusBalance,
                Birthdate = player.Birthdate,
                Country = player.Country,
                Registered = player.Registered,
                ExternalGameSessionId = "MYEXTERNALGAMESESSION"
            };

            // Note, we do not set Code or Message, by default this will be "OK" values.
            return response;

        }
    }

}