
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IntegrationSample
{
    /// <summary>
    /// Our Example Transaction. Feel free to model this as you please, as long as it works with the API
    /// </summary>
    public class Transaction
    {

        [Key]
        public long Id { get; set; }
        public long ExternalTransactionId { get; set; }
        public long PlayerId { get; set; }
        public long? RoundId { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime Time { get; set; }
        [Column(TypeName = "decimal(19,4)")]
        public decimal Amount { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }


    }

    public enum TransactionType
    {
        Debit = 0,
        Credit = 1
    }
    public enum TransactionStatus
    {

        Completed = 0,
        Cancelled = 1
    }
}