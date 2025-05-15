using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SushiManagementSystem.Application.DTOs
{
    public class InventoryReportDto
    {
        public required string ItemName { get; set; }
        public int Quantity { get; set; }
        public required string Unit { get; set; }
    }
}