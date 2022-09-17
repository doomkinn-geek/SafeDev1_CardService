using AutoMapper;
using CardService.Data;
using CardService.Models;
using CardService.Models.Requests;
using CardService.Services;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CardService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardRepositoryService _cardRepositoryService;
        private readonly ILogger<CardController> _logger;
        private readonly IValidator<CreateCardRequest> _cardRequestValidator;
        private readonly IMapper _mapper;

        public CardController(
            ILogger<CardController> logger,
            ICardRepositoryService cardRepositoryService,
            IValidator<CreateCardRequest> cardRequestValidator,
            IMapper mapper)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
            _cardRequestValidator = cardRequestValidator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IDictionary<string, string[]>), StatusCodes.Status400BadRequest)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            ValidationResult validationResult = _cardRequestValidator.Validate(request);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.ToDictionary());
            try
            {
                //var cardId = _cardRepositoryService.Create(new Card
                //{
                //    ClientId = request.ClientId,
                //    CardNo = request.CardNo,
                //    ExpDate = request.ExpDate,
                //    CVV2 = request.CVV2
                //});
                var cardId = _cardRepositoryService.Create(_mapper.Map<Card>(request));
                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] int clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = _mapper.Map<List<CardDto>>(cards)
                    //Cards = cards.Select(card => new CardDto
                    //{
                    //    CardId = card.CardId,
                    //    CardNo = card.CardNo,
                    //    CVV2 = card.CVV2,
                    //    Name = card.Name,
                    //    ExpDate = card.ExpDate.ToString("MM/yy")
                    //}).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }
    }
}
