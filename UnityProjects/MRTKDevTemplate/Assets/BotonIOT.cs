using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotonIOT : MonoBehaviour
{
    [SerializeField] GameObject askForNewSessionPanel;

    public void ActiveAskPanel()
    {
        if(!askForNewSessionPanel.activeInHierarchy)
        {
            askForNewSessionPanel.SetActive(true);
        }
    }

    public void DisableAskPanel()
    {
        if(askForNewSessionPanel.activeInHierarchy)
        {
            askForNewSessionPanel.SetActive(false);
        }
    }
}
