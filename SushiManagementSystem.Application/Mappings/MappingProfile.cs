using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Domain.Entities;

namespace SushiManagementSystem.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            // Позже добавь маппинги для других сущностей, например:
            // CreateMap<Order, OrderDto>().ReverseMap();
        }
    }
}