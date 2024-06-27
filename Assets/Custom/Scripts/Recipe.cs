using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public List<Ingredient.Name> ingredients;
    public GameObject resultObjectPrefab;
    public Category category;

    public Sprite image;
    public string questDescription = "Quest";

    public enum Category
    {
        First,
        Second,
        Final,
        Trading,
        Start,
        Test // todo remove before commit
    }

    public Drink GetDrink()
    {
        Drink drink = resultObjectPrefab.GetComponent<Drink>();
        
        if(drink == null) {
            Debug.Log("resultObjectPrefab is invalid and don't has 'Drink' Script.");
        }
        
        return drink;
    }

}