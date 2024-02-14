using System.ComponentModel.DataAnnotations;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Cancel a round by rolling back all transactions with that round id
    /// </summary>
    public class CancelRoundRequest : BaseIntegrationRequest
    {
        /// <summary>
        /// The player id the integrating system has provided for the player
        /// </summary>
        /// <value></value>
        [Required]
        public string ExternalPlayerId { get; set; }
        /// <summary>
        /// RoundId where all transactions on should be rolled back.
        /// </summary>
        /// <value></value>
        [Required]
        public long? RoundId { get; set; }

    }
}