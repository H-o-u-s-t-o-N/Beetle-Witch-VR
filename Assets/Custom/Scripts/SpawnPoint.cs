using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Ingredient.Name ingredientName;
    public bool isUnlocked = false;
    public RecipeDatabase recipeDatabase;

    private GameObject currentIngredient;

    private void Start()
    {
        if (isUnlocked)
        {
            SpawnIngredient();
        }
    }

    public void SpawnIngredient()
    {
        if (currentIngredient == null)
        {
            GameObject ingredientPrefab = GetIngredientPrefab(ingredientName);
            if (ingredientPrefab != null)
            {
                currentIngredient = Instantiate(ingredientPrefab, transform.position, transform.rotation);
            }
        }
    }

    public void DestroyIngredient()
    {
        if (currentIngredient != null)
        {
            Destroy(currentIngredient);
            currentIngredient = null;
        }
    }

    private GameObject GetIngredientPrefab(Ingredient.Name ingredientName)
    {
        foreach (var recipe in recipeDatabase.recipes)
        {
            if (recipe.resultObjectPrefab.GetComponent<Ingredient>().name == ingredientName)
            {
                return recipe.resultObjectPrefab;
            }
        }
        return null;
    }
}
