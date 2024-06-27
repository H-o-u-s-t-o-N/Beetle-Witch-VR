using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrading : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;
    public TradeSpawnPoint spawnPoint;

    private List<Recipe> tradingList;
    private GameObject noInfo;
    private GameObject yesInfo;


    void Start()
    {
        this.tradingList = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Trading);
        this.noInfo = transform.Find("NoInfoCard").gameObject;
        this.yesInfo = transform.Find("YesInfoCard").gameObject;

        noInfo.SetActive(false);
        yesInfo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            if (CheckTrade(ingredient))
            {
                StartCoroutine(showInfo(yesInfo));
            }
            else
            {
                StartCoroutine(showInfo(noInfo));
            }
        }
        else
        {
            StartCoroutine(showInfo(noInfo));
        }

        Destroy(other.gameObject);
    }

    private bool CheckTrade(Ingredient ingredient)
    {
        foreach (var recipe in tradingList)
        {
            if (IsTradeCorrect(recipe, ingredient))
            {
                spawnPoint.SpawnIngredient(recipe.resultObjectPrefab);
                return true;
            }
        }
        return false;
    }

    private bool IsTradeCorrect(Recipe recipe, Ingredient ingredient)
    {
        if (recipe.ingredients.Count != 1)
        {
            return false;
        }

        Ingredient.Name fromRecipe = recipe.ingredients[0];

        if (fromRecipe == ingredient.name)
        {
            return true;
        }
        else return false;
    }

    IEnumerator showInfo(GameObject info)
    {
        info.SetActive(true);
        yield return new WaitForSeconds(2);
        info.SetActive(false);
    }

}
