using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepManager : MonoBehaviour
{
    [Serializable]
    class Step
    {
        [SerializeField]
        public GameObject stepObject;

        [SerializeField]
        public string buttonText;
    }

    [SerializeField]
    public TextMeshProUGUI m_StepButtonTextField;

    [SerializeField]
    List<Step> m_StepList = new List<Step>();

    int m_CurrentStepIndex = 0;

    public void Next()
    {
        m_StepList[m_CurrentStepIndex].stepObject.SetActive(false);
        m_CurrentStepIndex = (m_CurrentStepIndex + 1) % m_StepList.Count;
        m_StepList[m_CurrentStepIndex].stepObject.SetActive(true);
        // m_StepButtonTextField.text = m_StepList[m_CurrentStepIndex].buttonText;
    }

    public void AddStep(GameObject stepObject, string buttonText)
    {
        Step newStep = new Step
        {
            stepObject = stepObject,
            buttonText = buttonText
        };
        m_StepList.Add(newStep);
    }

}

