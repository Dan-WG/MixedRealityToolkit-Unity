using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamenRespuestas : MonoBehaviour
{

    public bool Correcta =  false; //Por default la respuesta estara marcada como respuesta erronea
    public Examen examen; //Referencia al script de Examen para actualizar score

    private void Start()
    {
        examen = GameObject.FindGameObjectWithTag("Scripts").GetComponent<Examen>();
    }
    public void Respuesta()
    {
        if (Correcta) //Si la respuesta es correcta, mandar interaccion y aumentar puntaje
        {
            Examen.Score++;
            Debug.Log(Examen.Score);
            examen.Responder();
        }
        else //si se responde incorrectamente, mando interaccion y solo mando degub de respuesta erronea
        {
            examen.Responder();
            Debug.Log("Respuesta Incorrecta");
        }

        examen.OpcionSelecionada.SetActive(false);
    }

    public void SeleccionarRespuesta()
    {
        
        ExamenNextButton.selectedOption = gameObject;
        examen.OpcionSelecionada.transform.parent = examen.ParentBoton.transform;
        examen.OpcionSelecionada.transform.position = gameObject.transform.position + new Vector3(-.05f, -.07f, -.02f);
        //examen.OpcionSelecionada.transform.localScale = gameObject.transform.localScale;
        examen.OpcionSelecionada.SetActive(true);
    }
}
