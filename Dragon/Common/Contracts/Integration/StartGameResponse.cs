using System;
using System.ComponentModel.DataAnnotations;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Start Game Integrate Response.
    /// </summary>
    public class StartGameResponse : BaseIntegrationResponse
    {
        /// <summary>
        /// PlayerId in the operators system.
        /// </summary>
        /// <value></value>
        [StringLength(128)]
        [Required]
        public string ExternalPlayerId { get; set; }
        /// <summary>
        /// Nickname of the player.
        /// </summary>
        /// <value></value>
        [StringLength(64)]
        public string Nickname { get; set; }
        /// <summary>
        /// three Letter currency code.
        /// example.
        /// EUR
        /// SEK
        /// DKK
        /// USD
        /// </summary>
        /// <value></value>
        [StringLength(3, MinimumLength = 3)]
        [Required]
        public string Currency { get; set; }
        /// <summary>
        /// Players Brand.
        /// </summary>
        /// <value></value>
        [StringLength(64)]
        public string Brand { get; set; }
        /// <summary>
        /// ISO Locale code. such as
        /// 
        /// zh_CN,
        /// zh_TW
        /// fr_fr
        /// en_US
        /// en_GB
        /// </summary>
        /// <value></value>
        public string Language { get; set; }
        /// <summary>
        /// Players Balance.
        /// </summary>
        /// <value></value>
        [Required]
        public Decimal Balance { get; set; }
        /// <summary>
        /// Players Bonus Balance.
        /// </summary>
        /// <value></value>
        public Decimal BonusBalance { get; set; }
        /// <summary>
        /// Players birth date, nullable. 
        /// </summary>
        /// <value></value>
        public DateTime? Birthdate { get; set; }
        /// <summary>
        /// Alpha 2 standard. Max 2 letters.
        /// https://www.iban.com/country-codes
        /// </summary>
        /// <value></value>
        [StringLength(2, MinimumLength = 2)]
        public string Country { get; set; }
        /// <summary>
        /// Date player was registered.
        /// </summary>
        /// <value></value>
        public DateTime? Registered { get; set; }
        /// <summary>
        /// External Game Session Id, operator side.
        /// </summary>
        /// <value></value>
        [StringLength(128)]
        public string ExternalGameSessionId { get; set; }

        /// <summary>
        /// Default Const.
        /// </summary>
        public StartGameResponse()
        {

        }
    }
}