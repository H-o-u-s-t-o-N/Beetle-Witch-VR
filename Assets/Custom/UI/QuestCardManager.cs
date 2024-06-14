using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(StepManager))]
public class QuestCardManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform cardParent;
    public GameObject startButton;
    public StepManager stepManager;

    private int currentQuestIndex = 0;


    public void CreateActiveQuestCard(Recipe recipe)
    {
        CreateQuestCard(recipe);
        stepManager.Next();

        if (startButton != null)
        {
            Destroy(startButton);
        }
    }

    void CreateQuestCard(Recipe recipe)
    {
        GameObject newCard = Instantiate(cardPrefab, cardParent);
        newCard.name = "Card " + currentQuestIndex;

        Transform modalText = newCard.transform.Find("Modal Text");
        if (modalText != null)
        {
            TextMeshProUGUI textComponent = modalText.GetComponent<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = recipe.questDescription;
            }
        }

        Transform imageHolder = newCard.transform.Find("Mask Background/Image Bounds/Image Card");
        if (imageHolder != null)
        {
            Image imageComponent = imageHolder.GetComponent<Image>();
            if (imageComponent != null)
            {
                imageComponent.sprite = recipe.image;
            }
        }

        stepManager.AddStep(newCard, "Step " + (currentQuestIndex + 1));

        currentQuestIndex++;
    }
}
