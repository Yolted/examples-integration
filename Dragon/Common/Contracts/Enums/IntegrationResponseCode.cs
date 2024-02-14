

namespace Dragon.Common.Contracts.Enums
{
    /// <summary>
    ///  Response codes for the DefaultIntegration B2B API.
    ///  Responses from the integrating operator/partner.
    /// 
    ///  Note numbers greater than 1000 should not be used. They are used internally for transport layer fault codes.
    /// </summary>
    public enum IntegrationResponseCode
    {
        /// <summary>
        /// OK. There was no issues, all good.
        /// </summary>
        Ok = 0,
        /// <summary>
        /// Invalid player token.
        /// </summary>
        InvalidToken = 1,
        /// <summary>
        /// Player is locked in the operator system.
        /// </summary>
        PlayerLocked = 2,
        /// <summary>
        /// Invalid player id.
        /// </summary>
        InvalidPlayerId = 3,
        /// <summary>
        /// Api is down. will default to this if the api responds with a none 200 or not at all.
        /// </summary>
        APIDown = 4,
        /// <summary>
        /// not enough money on player account
        /// </summary>
        InsufficientBalance = 5,
        /// <summary>
        /// player has reached limits.
        /// </summary>
        LimitsReached = 6,
        /// <summary>
        /// time limit has been exceeded
        /// </summary>
        TimeLimitReached = 7,
        /// <summary>
        /// Will Ask player to contact support.
        /// </summary>
        ContactSupport = 8,
        /// <summary>
        /// Generic error of some sort.
        /// </summary>
        Error = 9,
        ///<exclude/>
        //INTERNAL
        Timeout = 10001,
        ///<exclude/>
        //none 200 response
        BadResponse = 10002
    }
}
