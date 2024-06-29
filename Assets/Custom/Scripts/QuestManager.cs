using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;
    public GameObjectSpawnPoint debugSpawnPoint;

    private IngredientManager ingredientManager;
    private QuestCardManager questCardManager;
    private Queue<Recipe> questQueue = new Queue<Recipe>();
    private int currentQuestIndex = 0;
    private Recipe currentQuestRecipe;

    void Start()
    {
        this.ingredientManager = FindObjectOfType<IngredientManager>();

        if (ingredientManager == null)
        {
            Debug.LogError("IngredientManager not found on the scene");
        }

        this.questCardManager = FindObjectOfType<QuestCardManager>();

        if (ingredientManager == null)
        {
            Debug.LogError("QuestCardManager not found on the scene");
        }

        GenerateQuests();
    }

    public void StartGame()
    {
        StartNextQuest();
    }

    public void EndGame()
    {
        Debug.Log("End game");
        SceneManager.LoadScene(2);
    }

    void GenerateQuests()
    {
        var startRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Start);
        var firstRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.First);
        var secondRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Second);
        var finalRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Final);

        AddRandomRecipesToQueue(startRecipes, 1);
        AddRandomRecipesToQueue(firstRecipes, 3);
        AddRandomRecipesToQueue(secondRecipes, 3);
        AddRandomRecipesToQueue(finalRecipes, 3);
    }

    void AddRandomRecipesToQueue(List<Recipe> recipes, int count)
    {
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, recipes.Count);
            questQueue.Enqueue(recipes[index]);
            recipes.RemoveAt(index);
        }
    }

    public void StartNextQuest()
    {
        if (questQueue.Count > 0)
        {
            this.currentQuestRecipe = questQueue.Dequeue();

            ingredientManager.SpawnByCategory(currentQuestRecipe.category);

            questCardManager.CreateActiveQuestCard(currentQuestRecipe);
            currentQuestIndex++;
        }
        else
        {
            EndGame();
        }
    }

    public void OnQuestCompleted()
    {
        StartNextQuest();
    }

    public Drink.Name GetExpectedDrinkName()
    {
        return this.currentQuestRecipe.GetDrink().name;
    }

    public void SpawnDrink()
    {
        debugSpawnPoint.SpawnIngredient(currentQuestRecipe.resultObjectPrefab);
    }

}
