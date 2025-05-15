using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;

namespace SushiManagementSystem.Application.Services
{
    public class ReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<InventoryReportDto>> GetInventoryReportAsync()
        {
            var inventories = await _unitOfWork.Inventories.GetAllAsync();
            return inventories.Select(i => new InventoryReportDto
            {
                ItemName = i.ItemName,
                Quantity = i.Quantity,
                Unit = i.Unit
            });
        }
    }
}