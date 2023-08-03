using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JSONExamen : MonoBehaviour
{
    public TextAsset textJSON; //Archivo JSON

    public Examen examen; //examen para acceder a preguntas y respuestas 

    [System.Serializable]
    public class DatosPregunta //Estructura de datos de JSON
    {
        public string Pregunta;
        public List<string> Respuestas;
        public string RespuestaCorrecta;
    }
     

    [System.Serializable]
    public class Preguntas
    {
        public DatosPregunta[] PreguntasExamen; //
    }

    public Preguntas preguntas = new Preguntas();
  

    // Start is called before the first frame update
    void Start()
    {  
        preguntas = JsonUtility.FromJson<Preguntas>(textJSON.text); //Lectura de la info del JSON

    }


    private void Update()
    {
        //TituloPanel.text = Preguntas.ExamenesDetalle_Contenedor[Examen.NumPregunta - 1].Titulo; //Cambio de texto de titulo de acuerdo a la diapositiva
        //examen.TextoPregunta.GetComponent<TextMeshProUGUI>().text = Preguntas.ExamenesDetalle_Contenedor[Examen.NumPregunta - 1].Descripcion; //Cambio de texto en el panel de acuerdo a el paso en el cual vayamos
    }
}
