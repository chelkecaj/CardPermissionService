﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface ICardRepository
    {
        Task<CardDetails?> GetCardDetails(string userId, string cardNumber);
    }
}
