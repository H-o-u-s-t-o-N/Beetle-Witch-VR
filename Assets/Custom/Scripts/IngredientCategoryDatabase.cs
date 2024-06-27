using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientCategoryDatabase", menuName = "ScriptableObjects/IngredientCategoryDatabase", order = 2)]
public class IngredientCategoryDatabase : ScriptableObject
{
    public List<Ingredient.Name> start;
    public List<Ingredient.Name> first;
    public List<Ingredient.Name> second;
    public List<Ingredient.Name> final;

}
