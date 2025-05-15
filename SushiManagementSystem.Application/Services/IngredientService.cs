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
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IngredientService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync()
        {
            var ingredients = await _unitOfWork.Ingredients.GetAllAsync();
            return _mapper.Map<IEnumerable<IngredientDto>>(ingredients);
        }

        public async Task<IngredientDto> GetIngredientByIdAsync(int id)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(id);
            return _mapper.Map<IngredientDto>(ingredient);
        }

        public async Task AddIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            await _unitOfWork.Ingredients.AddAsync(ingredient);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateIngredientAsync(IngredientDto ingredientDto)
        {
            var ingredient = _mapper.Map<Ingredient>(ingredientDto);
            _unitOfWork.Ingredients.Update(ingredient);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteIngredientAsync(int id)
        {
            var ingredient = await _unitOfWork.Ingredients.GetByIdAsync(id);
            if (ingredient != null)
            {
                _unitOfWork.Ingredients.Delete(ingredient);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}