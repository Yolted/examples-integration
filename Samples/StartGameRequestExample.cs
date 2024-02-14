using Dragon.Common.Contracts.Enums;
using Dragon.Common.Contracts.Integration.Classes;
using Swashbuckle.AspNetCore.Filters;

namespace IntegrationSample
{

    public class StartGameRequestExample : IExamplesProvider<StartGameRequest>
    {
        public StartGameRequest GetExamples()
        {
            return new StartGameRequest
            {
                Token = "ASENSITIVESHORTLIVEDTOKENPASSEDTHROUGHGAMELAUNCH",
                Context = "acontext",
                IP = "233.233.233.233",
                Language = "en_GB",
                GameId = "firstgame",
                Channel = Channel.Mobile,
                Key = "ASUPERSECRETAPIKEY"

            };
        }
    }
}