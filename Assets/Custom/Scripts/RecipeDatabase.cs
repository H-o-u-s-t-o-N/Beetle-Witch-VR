using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeDatabase", menuName = "ScriptableObjects/RecipeDatabase", order = 1)]
public class RecipeDatabase : ScriptableObject
{
    public List<Recipe> recipes;

    public List<Recipe> GetCauldronRecipes()
    {
        return recipes.Where(r => r.category != Recipe.Category.Trading).ToList();
    }

}
