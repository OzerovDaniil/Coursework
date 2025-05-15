using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SushiManagementSystem.Application.DTOs;

namespace SushiManagementSystem.Application.Interfaces
{
    public interface IRecipeItemService
    {
        Task<IEnumerable<RecipeItemDto>> GetAllRecipeItemsAsync();
        Task<RecipeItemDto> GetRecipeItemByIdAsync(int id);
        Task AddRecipeItemAsync(RecipeItemDto recipeItemDto);
        Task UpdateRecipeItemAsync(RecipeItemDto recipeItemDto);
        Task DeleteRecipeItemAsync(int id);
    }
}