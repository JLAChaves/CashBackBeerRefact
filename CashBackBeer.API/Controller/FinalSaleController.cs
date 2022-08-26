using Bs2.Core2.Utilities.Messaging.Producers;
using CashBackBeer.Application.ForAPI.FinalSaleHandler.CreateFinalSale;
using CashBackBeer.Application.Models;
using CashBackBeer.Application.Models.Msg;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashbackBeer.API.Controllers
{
    [Route("api/sale")]
    [ApiController]
    public class FinalSaleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProducer<FinalSaleCreatedMsg> _producer;
        public FinalSaleController(IMediator mediator, IProducer<FinalSaleCreatedMsg> producer)
        {
            _mediator = mediator;
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody()] FinalSaleDTO finalSaleDTO, CancellationToken cancellationToken)
        {
            CreateFinalSaleRequestDTO createFinalSaleRequestDTO = new CreateFinalSaleRequestDTO()
            {
                Date = finalSaleDTO.Date,
                Items = finalSaleDTO.Items
            };

            await _mediator.Send(createFinalSaleRequestDTO);

            await _producer.ProduceAsync(new ProductionEnvelope<FinalSaleCreatedMsg>(new FinalSaleCreatedMsg()
            {
                Date = createFinalSaleRequestDTO.Date,
                Items = createFinalSaleRequestDTO.Items,
            },
            Guid.NewGuid().ToString()), cancellationToken);

            return Created("", finalSaleDTO);
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        //{
        //    var finalSaleDTO = await _finalSaleService.GetFinalSaleDTOByIdAsync(id, cancellationToken);

        //    if (finalSaleDTO == null) return BadRequest("Invalid id");

        //    return Ok(finalSaleDTO);
        //}

        //[HttpGet]
        //public IActionResult GetAll([FromQuery()] PaginationParams pagination, CancellationToken cancellationToken)
        //{
        //    if (pagination.Limit <= 0 || pagination.Page <= 0) return BadRequest();

        //    var getAll = _finalSaleService.GetFinalSaleDTOAll(pagination, cancellationToken);

        //    return Ok(getAll);
        //}

        //[HttpGet]
        //public IActionResult GetFinalSaleByDate([FromQuery()] PaginationParams pagination, [FromQuery()] DateTime? minDate, [FromQuery()] DateTime? maxDate)
        //{
        //    if (pagination.Limit <= 0 || pagination.Page <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var getAll = _finalSaleService.GetFinalSaleByDate(pagination, minDate, maxDate);
        //    return Ok(getAll);
        //}
    }
}
