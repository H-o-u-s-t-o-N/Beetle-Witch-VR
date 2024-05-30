using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public IngredientManager ingredientManager;
    public RecipeDatabase recipeDatabase;
    
    private List<Ingredient.Name> currentIngredients = new List<Ingredient.Name>();

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            currentIngredients.Add(ingredient.name);

            Destroy(other.gameObject);
            CheckIngredients();

            ingredientManager.RespawnAllIngredients();
        }
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-other.transform.forward * 1, ForceMode.Impulse);
            }
        }
    }

    private void CheckIngredients()
    {
        foreach (var recipe in recipeDatabase.recipes)
        {
            if (IsRecipeCorrect(recipe))
            {
                // CreateResultObject(recipe.resultObjectPrefab); for test
                UnlockNewIngredients(recipe.resultObjectPrefab.GetComponent<Ingredient>().name);
                return;
            }
        }
    }

    private bool IsRecipeCorrect(Recipe recipe)
    {
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            return false;
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (!currentIngredients.Contains(ingredient))
            {
                return false;
            }
        }

        return true;
    }

    private void CreateResultObject(GameObject resultObjectPrefab)
    {
        Instantiate(resultObjectPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        currentIngredients.Clear();
    }

    private void UnlockNewIngredients(Ingredient.Name newIngredient)
    {
        ingredientManager.UnlockIngredient(newIngredient);
    }
}
