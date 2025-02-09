using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public record CardDetails(string CardNumber, CardType CardType, CardStatus CardStatus, bool IsPinSet);
}
