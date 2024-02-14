
using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationSample
{
    /// <summary>
    /// Our Example player.
    /// Feel free to model this as you please, as long as it works with the API
    /// </summary>
    public class Player
    {

        [Key]
        public long Id { get; set; }
        public string Brand { get; set; }
        public string Nickname { get; set; }
        public string Country { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
        public DateTime? Registered { get; set; }
        public DateTime? Birthdate { get; set; }
        public Boolean Locked { get; set; }
        public decimal Balance { get; set; }
        public decimal BonusBalance { get; set; }
        public string Token { get; set; }
    }

}