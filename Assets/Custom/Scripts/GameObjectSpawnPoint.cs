using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawnPoint : MonoBehaviour
{
    [SerializeField] private AudioClip soundSpawn;

    public void SpawnIngredient(GameObject ingredient)
    {
        if (ingredient != null)
        {
            StartCoroutine(spawn(ingredient));
        }
    }

    private IEnumerator spawn(GameObject ingredient)
    {
        yield return new WaitForSeconds(1);
        var spawned = Instantiate(ingredient, transform.position, transform.rotation);
        var rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.up * 2.5f, ForceMode.Impulse);
        }

        SoundFXManager.instance.PlayClip(soundSpawn, transform, 0.5f);
    }
}