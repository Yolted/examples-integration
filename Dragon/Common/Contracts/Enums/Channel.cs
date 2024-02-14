

using System.ComponentModel.DataAnnotations;

namespace Dragon.Common.Contracts.Enums
{
    /// <summary>
    /// The different channels that the games can be launched on. This controls certain behavior and placement of game elements as well as the UI of the game.
    /// </summary>
    public enum Channel

    {
        /// <summary>
        /// Desktop channel
        /// </summary>
        [Display(Name = "Desktop")]
        Desktop = 0,
        /// <summary>
        /// Mobile Channel. Also includes Tablets.
        /// </summary>
        [Display(Name = "Mobile")]
        Mobile = 1,
        /// <summary>
        /// Terminal/Cabinet channel, for land based gaming.
        /// </summary>
        [Display(Name = "Terminal")]
        Terminal = 2,
        /// <summary>
        /// Mini games channel, as a side bet game etc.
        /// </summary>
        [Display(Name = "Mini")]
        Mini = 3
    }
}
