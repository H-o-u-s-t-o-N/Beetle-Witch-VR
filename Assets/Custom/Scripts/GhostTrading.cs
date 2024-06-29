using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrading : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;

    [SerializeField] private AudioClip soundYes;
    [SerializeField] private AudioClip soundNo;
    private GameObjectSpawnPoint spawnPoint;
    private List<Recipe> tradingList;
    private GameObject noInfo;
    private GameObject yesInfo;


    void Start()
    {
        this.tradingList = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Trading);
        this.noInfo = transform.Find("NoInfoCard").gameObject;
        this.yesInfo = transform.Find("YesInfoCard").gameObject;

        this.spawnPoint = transform.GetComponentsInChildren<GameObjectSpawnPoint>()[0];

        if (spawnPoint == null)
        {
            Debug.Log("GhostTrading Initial Error. Missing GameObjectSpawnPoint.");
        }

        noInfo.SetActive(false);
        yesInfo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var ingredient = other.GetComponent<Ingredient>();
        if (ingredient != null)
        {
            if (CheckTrade(ingredient))
            {
                SoundFXManager.instance.PlayClip(soundYes, transform, 0.5f);
                StartCoroutine(showInfo(yesInfo));
            }
            else
            {
                SoundFXManager.instance.PlayClip(soundNo, transform, 0.5f);
                StartCoroutine(showInfo(noInfo));
            }
        }
        else
        {
            SoundFXManager.instance.PlayClip(soundNo, transform, 0.5f);
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

        var fromRecipeName = recipe.ingredients[0];

        if (fromRecipeName == ingredient.name)
        {
            return true;
        }
        else return false;
    }

    private IEnumerator showInfo(GameObject info)
    {
        info.SetActive(true);
        yield return new WaitForSeconds(2);
        info.SetActive(false);
    }

}
