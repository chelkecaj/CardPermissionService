using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    [ApiController]
    [Route("cards/permissions")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;
        private readonly ILogger<CardController> _logger;

        public CardController(ICardService cardService, ILogger<CardController> logger)
        {
            _cardService = cardService;
            _logger = logger;
        }

        [HttpGet("{userId}/{cardNumber}")]
        public async Task<IActionResult> GetAllowedActions(string userId, string cardNumber)
        {
            var cardDetails = await _cardService.GetCardDetails(userId, cardNumber);
            if (cardDetails == null)
            {
                _logger.LogWarning($"Card {cardNumber} for user {userId} not found.");
                return NotFound(new { message = $"Card {cardNumber} for user {userId} not found." });
            }

            var allowedActions = _cardService.GetAllowedActions(cardDetails);
            return Ok(new { actions = allowedActions });
        }
    }
}
