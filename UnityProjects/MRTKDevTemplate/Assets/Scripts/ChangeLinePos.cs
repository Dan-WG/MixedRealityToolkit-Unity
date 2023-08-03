using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLinePos : MonoBehaviour
{
    
    public GameObject LinePos1, LinePos2;
    private void OnTriggerEnter(Collider other)//posicion del line renderer del lado derecho del panel
    {
        if(other.gameObject.tag == "LadoIzq")
        {
            StoryTelling.PanelLinePos = LinePos1;
        }
    }

    private void OnTriggerExit(Collider other) //posicion del line renderer al lado izq del panel 
    {
        if (other.gameObject.tag == "LadoIzq")
        {
            StoryTelling.PanelLinePos = LinePos2;
        }
    }
}
