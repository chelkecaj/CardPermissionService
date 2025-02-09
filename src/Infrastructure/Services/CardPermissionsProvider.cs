using Domain.Models;
using Infrastructure.Config;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CardPermissionsProvider
    {
        private readonly ILogger<CardPermissionsProvider> _logger;
        private readonly string _configPath;

        public CardPermissionsProvider(ILogger<CardPermissionsProvider> logger)
        {
            _logger = logger;
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            _configPath = Path.Combine(basePath, "Config", "CardPermissions.json");

            if (!File.Exists(_configPath))
            {
                _logger.LogError($"Card configuration file not found: {_configPath}");
                throw new FileNotFoundException($"Card configuration file not found: {_configPath}");
            }
        }

        public IEnumerable<CardPermission> GetCardPermissions()
        {
            var json = File.ReadAllText(_configPath);
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
            };

            var cardPermissions = JsonSerializer.Deserialize<CardPermissionsWrapper>(json, options)?.CardPermissions ?? new List<CardPermission>();

            ValidateCardPermissions(cardPermissions);

            return cardPermissions;
        }
        private void ValidateCardPermissions(IEnumerable<CardPermission> cardPermissions)
        {
            foreach (var permission in cardPermissions)
            {
                foreach (var cardType in permission.CardTypes)
                {
                    if (!Enum.IsDefined(typeof(CardType), cardType))
                    {
                        _logger.LogError($"Invalid CardType '{cardType}' found in permission '{permission.Name}'.");
                        throw new InvalidDataException($"Invalid CardType '{cardType}' found in permission '{permission.Name}'.");
                    }
                }
                foreach (var cardStatus in permission.CardStatuses)
                {
                    if (!Enum.IsDefined(typeof(CardStatus), cardStatus))
                    {
                        _logger.LogError($"Invalid CardType '{cardStatus}' found in permission '{permission.Name}'.");
                        throw new InvalidDataException($"Invalid CardStatus '{cardStatus}' found in permission '{permission.Name}'.");
                    }
                }
            }
        }
    }
}
