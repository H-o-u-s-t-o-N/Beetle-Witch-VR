using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public RecipeDatabase recipeDatabase;
    public QuestCardManager questCardManager;
    public IngredientManager ingredientManager;
    public GameObjectSpawnPoint debugSpawnPoint;

    private Queue<Recipe> questQueue = new Queue<Recipe>();
    private int currentQuestIndex = 0;
    private Recipe currentQuestRecipe;

    void Start()
    {
        GenerateQuests();
    }

    public void StartGame()
    {
        StartNextQuest();
    }

    void GenerateQuests()
    {
        List<Recipe> startRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Start);
        List<Recipe> firstRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.First);
        List<Recipe> secondRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Second);
        // List<Recipe> finalRecipes = recipeDatabase.recipes.FindAll(r => r.category == Recipe.Category.Final);

        AddRandomRecipesToQueue(startRecipes, 1);
        AddRandomRecipesToQueue(firstRecipes, 3);
        AddRandomRecipesToQueue(secondRecipes, 3);
        // AddRandomRecipesToQueue(finalRecipes, 1);
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
            Debug.Log("End game");
        }
    }

    public void OnQuestCompleted()
    {
        StartNextQuest();
    }

    public Drink GetExpectedDrink()
    {
        return this.currentQuestRecipe.GetDrink();
    }

    public void SpawnDrink()
    {
        debugSpawnPoint.SpawnIngredient(currentQuestRecipe.resultObjectPrefab);
    }

}
