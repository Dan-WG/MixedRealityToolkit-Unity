using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelAR_evento : MonoBehaviour
{
    public delegate void EnablePanel();
    public static event EnablePanel panelEnabled;

    //Trigerear un evento que notifique que el panel se ha activado.
    private void Start() 
    {
        if (panelEnabled != null)
        {
            panelEnabled();
        }
    }
}
