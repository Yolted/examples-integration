using Dragon.Common.Contracts.Enums;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Credit player account
    /// </summary>
    public class CreditRequest : BaseIntegrationRequest
    {
        /// <summary>
        /// Yolteds transaction id
        /// </summary>
        /// <value></value>
        public long TransactionId { get; set; }
        /// <summary>
        /// Nr of rounds played 
        /// </summary>
        /// <value></value>
        public int NrOfRounds { get; set; }
        /// <summary>
        /// Round the transaction was made for.
        /// </summary>
        /// <value></value>
        public long? RoundId { get; set; }
        /// <summary>
        /// Players ID in the operators system
        /// </summary>
        /// <value></value>
        public string ExternalPlayerId { get; set; }
        /// <summary>
        /// Context round was played on
        /// </summary>
        /// <value></value>
        public string Context { get; set; }
        /// <summary>
        /// Game round was played on
        /// </summary>
        /// <value></value>
        public string GameId { get; set; }
        /// <summary>
        /// Channel round was played on
        /// </summary>
        /// <value></value>
        public Channel Channel { get; set; }
        /// <summary>
        /// Total Amount that should be credited to players account
        /// </summary>
        /// <value></value>
        public decimal Amount { get; set; }
        /// <summary>
        /// Currency of round
        /// </summary>
        /// <value></value>
        public string Currency { get; set; }
        /// <summary>
        /// Game Session  round was played on
        /// </summary>
        /// <value></value>
        public long GamesessionId { get; set; }
        /// <summary>
        /// Freegameofferid/promotionofferid if the round was played on any.
        /// </summary>
        /// <value></value>
        public long? FreeGameOfferId { get; set; }
        /// <summary>
        /// Indicates if this debit request is a buyin, meaning the money is transferred into an account that will be used to play multiple rounds.
        /// This is used mainly by blitz buy in function that can greatly increase the speed of blitz and minimize the amount of requests.
        /// 
        /// The integrating system is responsible to apply any limits on buy in transactions such as for example if it limits bets including bonus money(mixed bets).
        /// if so the integrating system can return Code 6 (limited reached) and the Yolted game system will fall back to use regular transactions.
        /// Note that for buyout a regular credit request is sent with the parent round id.
        /// </summary>
        /// <value></value>
        public bool BuyIn { get; set; }
        /// <summary>
        /// Only relevant when buyin is true.
        /// The actual amount that was betted. Used to calculate bonus and other contributions.
        /// </summary>
        /// <value></value>
        public decimal? TotalLoss { get; set; }
        /// <summary>
        /// Only relevant when buyin is true.
        /// The actual amount that was won, Same as the Amount minus amount that was left for rounds that where not played.
        /// </summary>
        /// <value></value>
        public decimal? TotalWin { get; set; }
        /// <summary>
        /// External Gamesession id, if such was passed on the game start.
        /// </summary>
        /// <value></value>
        public string ExternalGameSessionId { get; set; }

    }
}