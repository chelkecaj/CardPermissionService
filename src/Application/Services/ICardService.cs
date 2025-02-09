using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface ICardService
    {
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
        IEnumerable<string> GetAllowedActions(CardDetails card);
    }
}
