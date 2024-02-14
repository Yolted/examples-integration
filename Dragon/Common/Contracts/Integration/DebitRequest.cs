using System.ComponentModel.DataAnnotations;
using Dragon.Common.Contracts.Enums;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Debit a players account request
    /// </summary>
    public class DebitRequest : BaseIntegrationRequest
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
        /// Amount that should be debited of the players account
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
        /// <summary>
        /// Indicates if this debit request is a buy in, meaning the money is transferred into an account that will be used to play multiple rounds
        /// This is used mainly by blitz buy in function that can greatly increase the speed of blitz and minimize the amount of requests.
        /// 
        /// for regular game play this will always be false or null (not included in the request).
        /// 
        /// The integrating system is responsible to apply any limits on buy in transactions such as for example if it limits bets including bonus money(mixed bets).
        /// if so the integrating system can return Code 6 (limited reached) and the Yolted game system will fall back to use regular transactions.
        /// Note that for buyout a regular credit request is sent with the parent round id.
        /// </summary>
        /// <value></value>
        public bool BuyIn { get; set; }
    }
}