using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public int maxIngredientsCount = 5;
    public RecipeDatabase recipeDatabase;
    public ParticleSystem particle;
    public GameObjectSpawnPoint drinkSpawnPoint;

    [SerializeField] private AudioClip soundIn;

    private IngredientManager ingredientManager;
    private List<Recipe> recipes;
    private List<Ingredient.Name> currentIngredients = new List<Ingredient.Name>();

    void Start()
    {
        this.ingredientManager = FindObjectOfType<IngredientManager>();

        if (ingredientManager == null)
        {
            Debug.LogError("IngredientManager not found on the scene");
        }

        this.recipes = recipeDatabase.GetCauldronRecipes();
    }

    private void OnTriggerEnter(Collider other)
    {
        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            currentIngredients.Add(ingredient.name);

            Destroy(other.gameObject);
            CheckIngredients();

            SoundFXManager.instance.PlayClip(soundIn, transform, 1f);

            StartCoroutine(RespawnIngredientsAfterFrame(ingredient.name));
        }
        else
        {
            var rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(-other.transform.forward * 1, ForceMode.Impulse);
            }
        }
    }

    private void CheckIngredients()
    {
        foreach (var recipe in recipes)
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

        var ingredientCounts = new Dictionary<Ingredient.Name, int>();
        foreach (var ingredient in currentIngredients)
        {
            if (ingredientCounts.ContainsKey(ingredient))
            {
                ingredientCounts[ingredient]++;
            }
            else
            {
                ingredientCounts[ingredient] = 1;
            }
        }

        foreach (var ingredient in recipe.ingredients)
        {
            if (ingredientCounts.ContainsKey(ingredient) && ingredientCounts[ingredient] > 0)
            {
                ingredientCounts[ingredient]--;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

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