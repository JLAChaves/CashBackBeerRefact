using Bs2.Core2.Utilities.Messaging.Producers;
using CashBackBeer.Application.ForAPI.CreateBeer;
using CashBackBeer.Application.ForAPI.GetBeerById;
using CashBackBeer.Application.Models;
using CashBackBeer.Application.Models.Msg;
using CashBackBeer.Domain.Entities.FinalSaleAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CashbackBeer.API.Controllers
{
    [Route("api/beer")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IProducer<BeerCreatedMsg> _producer;
        public BeerController(IMediator mediator, IProducer<BeerCreatedMsg> producer)
        {
            _mediator = mediator;
            _producer = producer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBeer([FromBody()] BeerDTO beerDTO, CancellationToken cancellationToken)
        {
            CreateBeerRequestDTO createBeerRequestDTO = new CreateBeerRequestDTO()
            {
                Type = beerDTO.Type,
                Value = beerDTO.Value,
                CashBackRequestDTO = new CashBackDTO()
                {
                    Percentage = beerDTO.CashBackDTO.Percentage,
                    Day = beerDTO.CashBackDTO.Day
                }
            };

            await _mediator.Send(createBeerRequestDTO, cancellationToken);


            await _producer.ProduceAsync(new ProductionEnvelope<BeerCreatedMsg>(new BeerCreatedMsg()
            {
                Type = beerDTO.Type,
                Value = beerDTO.Value,
                CashBack = new CashBack() { Day = beerDTO.CashBackDTO.Day, Percentage = beerDTO.CashBackDTO.Percentage }
            },
            Guid.NewGuid().ToString()),
            cancellationToken);

            return Created("", beerDTO);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll([FromQuery()] PaginationParams pagination, [FromQuery()] BeerType beerType, CancellationToken cancellationToken)
        //{
        //    if (pagination.Limit <= 0 || pagination.Page <= 0)
        //    {
        //        return BadRequest();
        //    }
        //    var allBeer = _beerService.GetBeersByTypeAsync(pagination, beerType, cancellationToken);
        //    return Ok(allBeer);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            GetBeerByIdRequestDTO getBeerByIdRequestDTO = new GetBeerByIdRequestDTO() { Id = id };
            var response = await _mediator.Send(getBeerByIdRequestDTO, cancellationToken);

            if (response == null) return BadRequest("Invalid id");

            BeerDTO beerDTO = new BeerDTO()
            {
                Type = response.Type,
                Value = response.Value,
                CashBackDTO = response.CashBackResponseDTO
            };

            return Ok();
        }
    }
}
