using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Config
{
    public class CardPermissionsWrapper
    {
        public List<CardPermission> CardPermissions { get; set; } = new();
    }
    public class CardPermission
    {
        public string Name { get; set; } = string.Empty;
        public List<CardType> CardTypes { get; set; } = new();
        public List<CardStatus> CardStatuses { get; set; } = new();
        public bool? IsPinSet { get; set; }
    }
}
