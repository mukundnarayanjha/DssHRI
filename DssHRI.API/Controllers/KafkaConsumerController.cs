using DssHRI.Application.Constants;
using DssHRI.Domain.MRM;
using Kafka.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DssHRI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KafkaConsumerController : ControllerBase
    {
        private readonly IKafkaConsumer<string, SASUrlRequest> _consumer;
        public KafkaConsumerController(IKafkaConsumer<string, SASUrlRequest> kafkaConsumer)
        {
            _consumer = kafkaConsumer;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            try
            {
                CancellationToken stoppingToken = new();
                await _consumer.Consume(KafkaTopics.SendNewFileToIDM, stoppingToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{(int)HttpStatusCode.InternalServerError} ConsumeFailedOnTopic - {KafkaTopics.RegisterUser}, {ex}");
            }
            return Ok("Welcome to Kafka Consumer Application.");
        }



        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]        
        //public IActionResult Get()
        //{
        //    return Ok("Welcome to Kafka Consumer Application.");
        //}
    }
}
