using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;
using SushiManagementSystem.Application.Interfaces;
using SushiManagementSystem.Domain.Entities;
using AutoMapper;

namespace SushiManagementSystem.Application.Services
{
    public class RecipeItemService : IRecipeItemService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecipeItemService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RecipeItemDto>> GetAllRecipeItemsAsync()
        {
            var recipeItems = await _unitOfWork.RecipeItems.GetAllAsync();
            return _mapper.Map<IEnumerable<RecipeItemDto>>(recipeItems);
        }

        public async Task<RecipeItemDto> GetRecipeItemByIdAsync(int id)
        {
            var recipeItem = await _unitOfWork.RecipeItems.GetByIdAsync(id);
            return _mapper.Map<RecipeItemDto>(recipeItem);
        }

        public async Task AddRecipeItemAsync(RecipeItemDto recipeItemDto)
        {
            var recipeItem = _mapper.Map<RecipeItem>(recipeItemDto);
            await _unitOfWork.RecipeItems.AddAsync(recipeItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateRecipeItemAsync(RecipeItemDto recipeItemDto)
        {
            var recipeItem = _mapper.Map<RecipeItem>(recipeItemDto);
            _unitOfWork.RecipeItems.Update(recipeItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteRecipeItemAsync(int id)
        {
            var recipeItem = await _unitOfWork.RecipeItems.GetByIdAsync(id);
            if (recipeItem != null)
            {
                _unitOfWork.RecipeItems.Delete(recipeItem);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}