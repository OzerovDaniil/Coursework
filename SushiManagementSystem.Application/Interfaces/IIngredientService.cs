using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllIngredientsAsync();
        Task<IngredientDto> GetIngredientByIdAsync(int id);
        Task AddIngredientAsync(IngredientDto ingredientDto);
        Task UpdateIngredientAsync(IngredientDto ingredientDto);
        Task DeleteIngredientAsync(int id);
    }
}
