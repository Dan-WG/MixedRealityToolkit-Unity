using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamenNextButton : MonoBehaviour
{
    public static GameObject selectedOption = null;

    //Al ser clickada la respuesta seleccionada se va a asignar a si misma como selectedOption 
    //Y este método se activa al presionar el botón de siguiente 
    public void AceptarRespuesta()
    {
        if (selectedOption != null)
        {
            selectedOption.GetComponent<ExamenRespuestas>().Respuesta();
        }
    }
}
