using System;

namespace Dragon.Common.Contracts.Integration.Classes
{   
    /// <summary>
    /// Players balance in the operators system
    /// </summary>
    public class BalanceResponse : BaseIntegrationResponse
    {
        /// <summary>
        /// Currency
        /// </summary>
        /// <value></value>
        public string Currency { get; set; }
        /// <summary>
        /// Balance
        /// </summary>
        /// <value></value>
        public Decimal Balance { get; set; }
        /// <summary>
        /// Bonus balance
        /// </summary>
        /// <value></value>
        public Decimal BonusBalance { get; set; }
    }
}