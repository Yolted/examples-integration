using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dragon.Common.Contracts.Integration.Classes;
using Microsoft.Extensions.Logging;
using Dragon.Services;

namespace IntegrationSample.Controllers
{
    [Route("api/dragon")]
    [ApiController]
    /// <summary>
    /// This is an example a controller implementing the Default Integration for our game system.
    /// 
    /// NOTE! that a lot logic in this code is made to give clarity and might not be the most optimal or the solution that works best for you.
    /// 
    /// NOTE!
    /// You should use the build int Authorization and Authentication system to verify the API Key (in Header) and not the way we do here.
    /// This is just an example to be overly clear.
    /// </summary>
    public class SeamlessController : ControllerBase
    {
        private readonly ILogger<SeamlessController> _logger;
        private readonly IStubbedIntegrationService _integrationService;
        private readonly string ourSecretKey = "ourSystemsSecretKey";

        //ourSystemsSecretKey
        public SeamlessController(ILogger<SeamlessController> logger, IStubbedIntegrationService integrationService)
        {
            _integrationService = integrationService;
            _logger = logger;
        }

        /// <summary>
        /// Login/Authenticate player.
        /// </summary>
        /// <remarks>This is triggered when a player starts a game and integrating system should validate the token and return with needed player information.</remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("startgame")]
        public async Task<ActionResult<StartGameResponse>> StartGame([FromBody] StartGameRequest request)
        {
            //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);

            return await _integrationService.StartGame(request);
        }

        /// <summary>
        /// Get the players current balance.
        /// </summary>
        /// <remarks>
        /// Can be triggered for multiple reasons when the game system needs to refresh the players balance. 
        /// Example is if the player runs out of balance, his account is credited and we can update without him having to reload the game.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("balance")]
        public async Task<ActionResult<BalanceResponse>> Balance([FromBody] BalanceRequest request)
        {
            //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);

            return await _integrationService.Balance(request);
        }

        /// <summary>
        /// Debits the players account.
        /// </summary>
        /// <remarks>
        /// <para>Debits/reserve the bet made by the player. Done when starting a game round or triggering a buy in to an account (see BLITZ buy in accounts).</para>
        /// <para>Make sure to develop support for multiple Credit calls on a round id since some games might use this. Relevant for gamble/collect features etc.</para>
        /// <para>For promotion/freegame rounds the freeGameOfferId parameter will have a value, this will be null if not linked to a promotion. Note that Amount will also be 0 during promotion rounds.</para>
        /// <para>WARNING! its very important that the integrating system validate the amount! Game system may send a request for debit that is higher than its last known balance</para>
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("debit")]
        public async Task<ActionResult<DebitResponse>> Debit([FromBody] DebitRequest request)
        {
            //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);

            return await _integrationService.Debit(request);
        }

        /// <summary>
        /// Credit players Account
        /// </summary>
        /// <remarks>
        /// Credit player after a round, includes the round result. It will send credit with amount 0 to signal that there was no win on the round.
        /// only a single Credit call is made per round.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("credit")]
        public async Task<ActionResult<CreditResponse>> Credit([FromBody] CreditRequest request)
        {

            //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);



            return await _integrationService.Credit(request);
        }

        /// <summary>
        /// Cancel a Round request.
        /// </summary>
        /// <remarks>
        /// Cancel a Round. Refund all the bets (debits) made on the player for this round.
        /// 
        /// This occur if there was an error during the round or if the integrating system did not respond properly on the original request.
        /// 
        /// Note, if no rounds are found, or they are allready cancelled you should still respond OK for it to be properly resolved.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("cancelround")]
        public async Task<ActionResult<CancelRoundResponse>> CancelRound([FromBody] CancelRoundRequest request)
        {
            //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);


            return await _integrationService.CancelRound(request);

        }

        /// <summary>
        /// Close game session
        /// </summary>
        /// <remarks>
        ///  Can occur when player exits game properly or the session gets closed due to age.
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
     /*   [HttpPost("closegamesession")]
        public async Task<ActionResult<CloseGameSessionResponse>> CloseGameSession([FromBody] CloseGameSessionRequest request)
        {
         //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);
            

            return await _integrationService.CloseGameSession(request);
        }*/


        /// <summary>
        /// Debit and Credit in single request. 
        /// </summary>
        /// <remarks>
        /// Designed to minimize the amount requests for games that do not depend on any user actions. Note that the round is played before this request is sent
        /// but no result is shown before it has returned an ok.
        /// 
        /// <para>IMPORTANT! WARNING!
        /// You have to validate that the debitAmount is greater or equal to the players balance before adding any Credit and return insufficient balance.
        /// if not. This to ensure that the players is not able to bet for money he does not have.
        /// Not Doing so will create a scenario where the player can in effect bet for free.</para>
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("debitcredit")]
        public async Task<ActionResult<DebitCreditResponse>> DebitCredit([FromBody] DebitCreditRequest request)
        {

            //Validate the Key/Api Key to ensure request is made by an authorized system.
            if (request.Key != ourSecretKey)
                return StatusCode(403);

            return await _integrationService.DebitCredit(request);
        }

    }
}
