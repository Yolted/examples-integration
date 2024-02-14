using System;
using System.ComponentModel.DataAnnotations;
using Dragon.Common.Contracts.Enums;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Integration base response
    /// </summary>
    public class BaseIntegrationResponse
    {
        /// <summary>
        /// Code, if call failed or was successful. Most be included in all responses.
        /// </summary>
        /// <value></value>
        [Required]
        public IntegrationResponseCode Code { get; set; }
        /// <summary>
        /// Message, null if ok.f
        /// </summary>
        /// <value></value>
        public string Message { get; set; }

        /// <summary>
        /// Default const.
        /// </summary>
        public BaseIntegrationResponse()
        {

        }
    }
}