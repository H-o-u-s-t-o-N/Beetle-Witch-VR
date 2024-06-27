using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public int maxIngredientsCount = 5;
    public RecipeDatabase recipeDatabase;
    public ParticleSystem particle;
    public GameObjectSpawnPoint drinkSpawnPoint;

    private IngredientManager ingredientManager;
    private List<Ingredient.Name> currentIngredients = new List<Ingredient.Name>();

    void Start()
    {
        ingredientManager = FindObjectOfType<IngredientManager>();

        if (ingredientManager == null)
        {
            Debug.LogError("IngredientManager not found on the scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            currentIngredients.Add(ingredient.name);

            Destroy(other.gameObject);
            CheckIngredients();

            StartCoroutine(RespawnIngredientsAfterFrame(ingredient.name));
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
                drinkSpawnPoint.SpawnIngredient(recipe.resultObjectPrefab);
                currentIngredients.Clear();
                return;
            }
        }

        if (currentIngredients.Count >= maxIngredientsCount)
        {
            currentIngredients.Clear();
        }
    }

    private bool IsRecipeCorrect(Recipe recipe)
    {
        if (currentIngredients.Count != recipe.ingredients.Count)
        {
            return false;
        }

        var tempIngredients = new List<Ingredient.Name>(currentIngredients);

        foreach (var ingredient in recipe.ingredients)
        {
            if (tempIngredients.Contains(ingredient))
            {
                tempIngredients.Remove(ingredient);
            }
            else
            {
                return false;
            }
        }

        return tempIngredients.Count == 0;
    }

    // private void CreateResultObject(GameObject resultObjectPrefab)
    // {
    //     Instantiate(resultObjectPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
    //     currentIngredients.Clear();
    // }

    // private void UnlockNewIngredients(Ingredient.Name newIngredient)
    // {
    //     ingredientManager.UnlockIngredient(newIngredient);
    // }

    private IEnumerator RespawnIngredientsAfterFrame(Ingredient.Name name)
    {
        yield return new WaitForEndOfFrame();
        ingredientManager.RespawnIngredient(name);
    }

    //--------- Prticle Effects
    private void Update()
    {
        if (currentIngredients.Count > 0)
        {
            ActivateEffect();
        }
        else
        {
            DeactivateEffect();
        }
    }

    private void ActivateEffect()
    {
        if (particle != null && !particle.isPlaying)
        {
            particle.Play();
        }
    }

    private void DeactivateEffect()
    {
        if (particle != null && particle.isPlaying)
        {
            particle.Stop();
        }
    }
}