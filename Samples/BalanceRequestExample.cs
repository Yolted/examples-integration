using Dragon.Common.Contracts.Integration.Classes;
using Swashbuckle.AspNetCore.Filters;

namespace IntegrationSample
{
 

     public class BalanceRequestExample : IExamplesProvider<BalanceRequest>
    {
        public BalanceRequest GetExamples()
        {
            return new BalanceRequest
            {
                ExternalPlayerId ="playersIdInyoursystem",
                Key = "ASUPERSECRETAPIKEY"

            };
        }
    }
}