using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Ingredient.Name ingredientName;
    public Ingredient prefab;

    private Ingredient currentIngredient;

    public void Lock()
    {
        DestroyIngredient();
    }

    public void Unlock()
    {
        DestroyIngredient();
        SpawnIngredient();
    }

    public void SpawnIngredient()
    {
        if (currentIngredient == null)
        {
            if (prefab != null)
            {
                var spawned = Instantiate(prefab, transform.position, transform.rotation);
                var rb = spawned.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(transform.up * 1.5f, ForceMode.Impulse);
                }
                this.currentIngredient = spawned;
            }
            else
            {
                Debug.Log("Spawn point is invalid: Missing Ingredient prefab");
            }
        }
    }

    public void DestroyIngredient()
    {
        if (currentIngredient != null)
        {
            Destroy(currentIngredient.gameObject);
            currentIngredient = null;
        }
    }

}