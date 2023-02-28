using Confluent.Kafka;
using DssHRI.application.Interfaces;
using DssHRI.Application.Constants;
using DssHRI.Domain.Models.MRM;
using DssHRI.Domain.MRM;
using Kafka.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DssHRI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRIController : ControllerBase
    {
        private readonly IKafkaProducer<string, AddDocument> _kafkaAddDocProducer;
        private readonly IKafkaProducer<string, SASUrlResponse> _kafkaMRMResponseProducer;
        private readonly IAzureStorage _storage;
        private readonly ILogger<HRIController> _logger;
        private readonly IMediator _mediator;
        public HRIController(
            IKafkaProducer<string, AddDocument> kafkaAddDocProducer,
            IKafkaProducer<string, SASUrlResponse> kafkaMRMResponseProducer,
            IAzureStorage storage,
            ILogger<HRIController> logger,
            IMediator mediator
            )
        {
            _kafkaAddDocProducer = kafkaAddDocProducer;
            _kafkaMRMResponseProducer = kafkaMRMResponseProducer;
            _storage = storage;
            _logger = logger;
            _mediator = mediator;
        }
        [HttpPost]
        [Route("AddDocument")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddDocument(AddDocument addDocument)
        {
            try
            {                
                var topicPart = new TopicPartition(KafkaTopics.AddDocument, new Partition(1));
                await _kafkaAddDocProducer.ProduceAsync(topicPart, null, addDocument);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            //var sasUrlDetails = await _mediator.Send(new GenerateSASUrlQuery());

            return Ok();
        }
    }
}
