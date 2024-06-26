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
        Start
    }

}