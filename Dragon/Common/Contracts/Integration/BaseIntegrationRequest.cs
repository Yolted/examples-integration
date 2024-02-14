
using System.ComponentModel.DataAnnotations;

namespace Dragon.Common.Contracts.Integration.Classes
{
    /// <summary>
    /// Base Request
    /// </summary>
    public class BaseIntegrationRequest
    {
        /// <summary>
        /// Api Key that should be validated by the operator to ensure that only our game system is allowed to make calls.
        /// Can and should be combined with IP white listing.
        /// 
        /// <para>NOTE! this is the same value that is also sent as an Http Header, "Authorization: Basic KEY"</para>
        /// </summary>
        /// <value></value>
        [Required]
        public string Key { get; set; }
    }
}