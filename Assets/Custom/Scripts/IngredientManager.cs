using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public List<SpawnPoint> spawnPoints;

    private void Update()
    {
        // todo here change time mechanic trigger
    }

    public void RespawnAllIngredients()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.isUnlocked)
            {
                spawnPoint.SpawnIngredient();
            }
        }
    }

    public void UnlockIngredient(Ingredient.Name ingredientName)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint.ingredientName == ingredientName)
            {
                spawnPoint.isUnlocked = true;
                spawnPoint.SpawnIngredient();
            }
        }
    }
}
