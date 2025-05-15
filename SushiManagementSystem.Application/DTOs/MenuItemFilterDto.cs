using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace SushiManagementSystem.Application.DTOs
{
    public class MenuItemFilterDto
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}