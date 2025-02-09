using Domain.Models;
using Infrastructure.Config;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly CardPermissionsProvider _permissionsProvider;
        private readonly ILogger<CardService> _logger;

        public CardService(ICardRepository cardRepository, CardPermissionsProvider permissionsProvider, ILogger<CardService> logger)
        {
            _cardRepository = cardRepository;
            _permissionsProvider = permissionsProvider;
            _logger = logger;
        }

        public async Task<CardDetails?> GetCardDetails(string userId, string cardNumber)
        {
            var cardDetails = await _cardRepository.GetCardDetails(userId, cardNumber);
            if (cardDetails == null)
            {
                _logger.LogWarning($"Card {cardNumber} for user {userId} not found.");
            }

            return cardDetails;
        }

        public IEnumerable<string> GetAllowedActions(CardDetails card)
        {
            var permissions = _permissionsProvider.GetCardPermissions();

            return permissions
                .Where(p =>
                    p.CardTypes.Contains(card.CardType) &&
                    p.CardStatuses.Contains(card.CardStatus) &&
                    (p.IsPinSet == null || p.IsPinSet == card.IsPinSet)
                )
                .Select(action => action.Name)
                .ToList();
        }
    }

}
