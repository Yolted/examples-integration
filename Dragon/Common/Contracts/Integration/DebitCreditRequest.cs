using System.ComponentModel.DataAnnotations;
using Dragon.Common.Contracts.Enums;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    ///   Used to trigger bet and win at the same time when there is no action in the game that can affect the outcome.
    ///   Such as slot game without gamble and collect
    ///   this would reduce the amount of requests needed between the systems.
    /// </summary>
    public class DebitCreditRequest : BaseIntegrationRequest
    {
        /// <summary>
        /// Yolted transaction id
        /// </summary>
        /// <value></value>
        [Required]
        public long TransactionId { get; set; }
        /// <summary>
        /// Round transaction was made for
        /// </summary>
        /// <value></value>
        public long? RoundId { get; set; }
        /// <summary>
        /// Players ID in the operators system
        /// </summary>
        /// <value></value>
        [Required]
        public string ExternalPlayerId { get; set; }
        /// <summary>
        /// Context the round was played on
        /// </summary>
        /// <value></value>
        public string Context { get; set; }
        /// <summary>
        /// Game the round was played on
        /// </summary>
        /// <value></value>
        [Required]
        public string GameId { get; set; }
        /// <summary>
        /// Channel the Round was played on
        /// </summary>
        /// <value></value>
        [Required]
        public Channel Channel { get; set; }
        /// <summary>
        /// The Bet amount, this amount should be credited to the players account. This will be 0 if player did not win.
        /// Cannot be negative.
        /// 
        /// IMPORTANT, if a player does not have this amount on their account they should respond with insufficient balance and the round will be cancelled.
        /// Not doing so will allow the player to possibly bet money he does not have.
        /// No result will have been shown to the player.
        /// </summary>
        /// <value></value>
        [Required]
        public decimal DebitAmount { get; set; }
        /// <summary>
        /// The Win amount, this amount should be added to the players account.
        /// Cannot be negative.
        /// </summary>
        /// <value></value>
        [Required]
        public decimal CreditAmount { get; set; }
        /// <summary>
        /// The result of the round, this can be negative!
        /// Negative if player lost money. win - bet, creditAmount-DebitAmount.
        /// </summary>
        /// <value></value>
        [Required]
        public decimal Amount { get; set; }
        /// <summary>
        /// Currency of the round
        /// </summary>
        /// <value></value>
        [Required]
        public string Currency { get; set; }
        /// <summary>
        /// GameSession the round was played on
        /// </summary>
        /// <value></value>
        public long GamesessionId { get; set; }
        /// <summary>
        /// Operators gamesession id if any.
        /// </summary>
        /// <value></value>
        public string ExternalGameSessionId { get; set; }
        /// <summary>
        /// Freegameofferid/promotionofferid if the round was played on any.
        /// </summary>
        /// <value></value>
        public long? FreeGameOfferId { get; set; }

    }
}