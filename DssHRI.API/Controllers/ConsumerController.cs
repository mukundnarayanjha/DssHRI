using DssHRI.Application.Queries;
using DssHRI.API.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DssHRI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        private IMediator _mediator;
        private readonly RequestQueryHandler _handler;
        public ConsumerController(RequestQueryHandler handler, IMediator mediator)
        {
            _handler = handler;
            _mediator = mediator;
        }

        /// <summary>
        ///    Consume data from a topic
        /// </summary>
        /// <param name="request">holds the name of target topic </param>
        /// <returns> consumed message from topic</returns>
        [HttpPost]
        public async Task<IActionResult> Consume(RequestQuery request)
        {
            try
            {
                var result = await _handler.Handle(request);
                return await Task.FromResult(Ok(result));
            }
            catch (Exception e)
            {
                return await Task.FromResult(StatusCode(500, e.ToString()));
            }
        }
        
        [HttpPost(nameof(ConsumeWithMediator))]
        public async Task<IActionResult> ConsumeWithMediator(RequestQuery request)
        {
            try
            {
                var result = await _mediator.Send(new GenerateSASUrlQuery());
                return await Task.FromResult(Ok(result));
            }
            catch (Exception e)
            {
                return await Task.FromResult(StatusCode(500, e.ToString()));
            }
        }
    }
}

