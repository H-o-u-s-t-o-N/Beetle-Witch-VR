using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private QuestManager questManager;
    private GameObject noInfo;
    private GameObject yesInfo;

    void Start()
    {
        this.noInfo = transform.Find("NoInfoCard").gameObject;
        this.yesInfo = transform.Find("YesInfoCard").gameObject;

        noInfo.SetActive(false);
        yesInfo.SetActive(false);

        this.questManager = FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("QuestManager not found on the scene");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var drink = other.GetComponent<Drink>();
        if (drink != null)
        {
            var expected = questManager.GetExpectedDrinkName();
            if (expected == drink.name)
            {
                questManager.OnQuestCompleted();
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

    IEnumerator showInfo(GameObject info)
    {
        info.SetActive(true);
        yield return new WaitForSeconds(2);
        info.SetActive(false);
    }

}
