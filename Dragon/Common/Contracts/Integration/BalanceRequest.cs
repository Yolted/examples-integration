using System.ComponentModel.DataAnnotations;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Balance Request
    /// </summary>
    public class BalanceRequest : BaseIntegrationRequest
    {
        /// <summary>
        /// Players ID in the operators system
        /// </summary>
        /// <value></value>
        [Required]
        public string ExternalPlayerId { get; set; }

    }
}