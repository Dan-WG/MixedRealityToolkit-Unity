using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSVG : MonoBehaviour
{
    public List<GameObject> Buttons = new List<GameObject> (); //GOs de texturas 

  
    public void ChangeIcon(int i) //funcion de boton a cambio de textura
    {
        foreach (GameObject go in Buttons)
        {
            go.SetActive(false);
        }
        Buttons[i].SetActive(true);
    }
}
