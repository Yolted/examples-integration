/*using System.Threading.Tasks;
using AutoMapper;
using Dragon.Services;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Dragon.Grpc.Services
{

    public class GrpcDefaultIntegrationService : DefaultIntegration.DefaultIntegrationBase
    {

        private readonly ILogger<GrpcDefaultIntegrationService> _logger;
        private readonly IStubbedIntegrationService _integrationService;
        private readonly IMapper _mapper;
        private readonly string ourSecretKey = "ourSystemsSecretKey";
        public GrpcDefaultIntegrationService(ILogger<GrpcDefaultIntegrationService> logger,
         IStubbedIntegrationService integrationService, IMapper mapper)
        {
            _integrationService = integrationService;
            _logger = logger;
            _mapper = mapper;
        }
        public async override Task<StartGameResponse> StartGame(
            StartGameRequest request, ServerCallContext context)
        {

            var t = await _integrationService.StartGame(_mapper.Map<Dragon.Common.Contracts.Integration.Classes.StartGameRequest>(request));

            if (t.Code == Common.Contracts.Enums.IntegrationResponseCode.InvalidToken)
            {
                var metadata = new Metadata
                    {
                        { "Code", t.Code.ToString() }
                    };
                throw new RpcException(new Status(StatusCode.Unauthenticated, "InvalidToken"), metadata);
            }



            return _mapper.Map<StartGameResponse>(t);   
        }
    }

}*/