using System.ComponentModel.DataAnnotations;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Debit response
    /// </summary>
    public class DebitResponse : BaseIntegrationResponse
    {
        /// <summary>
        /// Players new balance
        /// </summary>
        /// <value></value>
        [Required]
        public decimal Balance { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        /// <value></value>
        [Required]
        public string Currency { get; set; }
        /// <summary>
        /// players new bonus balance
        /// </summary>
        /// <value></value>
        public decimal BonusBalance { get; set; }
        /// <summary>
        /// External Transaction Id, if operator wants to trace their own id on transaction.
        /// </summary>
        /// <value></value>
        public string ExternalTransactionId { get; set; }

    }
}