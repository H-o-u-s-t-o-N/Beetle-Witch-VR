using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public QuestManager questManager;
    private GameObject noInfo;
    private GameObject yesInfo;

    void Start()
    {
        this.noInfo = transform.Find("NoInfoCard").gameObject;
        this.yesInfo = transform.Find("YesInfoCard").gameObject;
        
        noInfo.SetActive(false);
        yesInfo.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Drink drink = other.GetComponent<Drink>();
        if (drink != null)
        {
            Drink expectedDrink = questManager.GetExpectedDrink();
            if (expectedDrink.name == drink.name)
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
