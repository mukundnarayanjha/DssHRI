using DssHRI.application.Interfaces;
using DssHRI.Application.Constants;
using DssHRI.Application.Queries;
using DssHRI.Domain.MRM;
using DssHRI.Domain.UserRegistration;
using Kafka.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DssHRI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaOperationController : ControllerBase
    {
        private readonly IKafkaProducer<string, RegisterUser> _kafkaProducer;
        private readonly IKafkaProducer<string, SASUrlRequest> _kafkaMRMProducer;
        private readonly IKafkaProducer<string, SASUrlResponse> _kafkaMRMResponseProducer;
        private readonly IAzureStorage _storage;
        private readonly ILogger<KafkaOperationController> _logger;
        private readonly IMediator _mediator;
        public KafkaOperationController(
            IKafkaProducer<string, RegisterUser> kafkaProducer,
            IKafkaProducer<string, SASUrlRequest> kafkaMRMProducer,
            IKafkaProducer<string, SASUrlResponse> kafkaMRMResponseProducer,
            IAzureStorage storage, ILogger<KafkaOperationController> logger, IMediator mediator
            )
        {
            _kafkaProducer = kafkaProducer;
            _kafkaMRMProducer = kafkaMRMProducer;
            _kafkaMRMResponseProducer = kafkaMRMResponseProducer;
            _storage = storage;
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("GenerateSASUrl")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateSASUrl(SASUrlRequest sasUrlRequest)
        {
            await _kafkaMRMProducer.ProduceAsync(KafkaTopics.SendNewFileToIDM, null, sasUrlRequest);

            // Get all files at the Azure Storage Location and return them
            Uri sasUrl = await _storage.GetServiceSasUriForContainer();
            SASUrlResponse sASUrlResponse = new()
            {
                SasUrl = sasUrl,
                UserName= sasUrlRequest.UserName
            };
            await _kafkaMRMResponseProducer.ProduceAsync(KafkaTopics.SASUrlResponse, null, sASUrlResponse);

            var sasUrlDetails = await _mediator.Send(new GenerateSASUrlQuery());

            return Ok(sasUrlDetails);
        }



        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ProduceMessage(RegisterUser request)
        {
            await _kafkaProducer.ProduceAsync(KafkaTopics.RegisterUser, null, request);

            return Ok("User Registration In Progress");
        }

    }
}
