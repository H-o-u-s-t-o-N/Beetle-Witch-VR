using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Recipe
{
    public List<Ingredient.Name> ingredients;
    public GameObject resultObjectPrefab;
}
