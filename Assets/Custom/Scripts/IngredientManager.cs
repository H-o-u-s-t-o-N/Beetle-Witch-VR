using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public IngredientCategoryDatabase database;

    private List<SpawnPoint> spawnPoints;

    void Start()
    {
        spawnPoints = new List<SpawnPoint>(FindObjectsOfType<SpawnPoint>());
    }

    // public void RespawnAllIngredients()
    // {
    //     foreach (var spawnPoint in spawnPoints)
    //     {
    //         if (spawnPoint.isUnlocked)
    //         {
    //             spawnPoint.SpawnIngredient();
    //         }
    //     }
    // }

    public void RespawnIngredient(Ingredient.Name ingredientName)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.ingredientName == ingredientName)
            {
                StartCoroutine(respawn(spawnPoint));
            }
        }
    }

    // public void UnlockIngredient(Ingredient.Name ingredientName)
    // {
    //     foreach (var spawnPoint in spawnPoints)
    //     {
    //         if (spawnPoint.ingredientName == ingredientName)
    //         {
    //             spawnPoint.isUnlocked = true;
    //             spawnPoint.SpawnIngredient();
    //         }
    //     }
    // }

    public void SpawnByCategory(Recipe.Category category)
    {
        List<Ingredient.Name> ingredientNames = null;

        switch (category)
        {
            case Recipe.Category.Start:
                ingredientNames = database.start;
                break;
            case Recipe.Category.First:
                ingredientNames = database.first;
                break;
            case Recipe.Category.Second:
                ingredientNames = database.second;
                break;
            case Recipe.Category.Final:
                ingredientNames = database.final;
                break;
        }

        if (ingredientNames != null)
        {
            foreach (var spawnPoint in spawnPoints)
            {
                if (ingredientNames.Contains(spawnPoint.ingredientName))
                {
                    spawnPoint.Unlock();
                }
                else
                {
                    spawnPoint.Lock();
                }
            }
        }
    }

    private IEnumerator respawn(SpawnPoint spawnPoint)
    {
        yield return new WaitForSeconds(1);
        spawnPoint.SpawnIngredient();
    }

}
