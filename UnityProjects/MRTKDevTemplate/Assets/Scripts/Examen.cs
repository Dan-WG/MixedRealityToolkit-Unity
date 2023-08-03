using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class Examen: MonoBehaviour
{
    //public List<PreguntasYRespuestas> PyR_Respaldo;
    //public List<PreguntasYRespuestas> PyR; //Lista de todas las preguntas y sus posibles respuestas

    //public List<GameObject> opciones; //Botones de respuesta 
    public static int NumPregunta; //pregunta a mostrar 
    public float PuntajeAprobatorio; //cuanto es el puntaje para aprobar el examen
    //public List<TextMeshProUGUI> TextoOpciones;
    private double calificacion;

    [SerializeField]
    private JSONExamen jSONExamen;

    [SerializeField]
    private GameObject BotonRespuesta;
    public GameObject ParentBoton, parentImagenSeleccion;

    [SerializeField] private GameObject PanelAprobado, PanelReprobado, PanelExamen, PanelInstrucciones; //Paneles de UI
    public GameObject OpcionSelecionada;
    [SerializeField] private TextMeshProUGUI TextoScoreAprobado, TextoScoreReprobado; //Texto de paneles 
    [SerializeField] private StoryTelling StoryTelling;

    public static float Score = 0; //Puntaje de examen

    public TextMeshProUGUI TextoPregunta; //Texto que se actualizara por cada pregunta

    // Start is called before the first frame update
    void Start()
    {
        //GenerarPregunta(); //Generar primera pregunta al iniciar 
        //PyR_Respaldo = PyR;
    }

    public void Responder() //Al responder, quito la pregunta de la lista para que no vuelva a salir y y vuelvo a generar otra pregunta
    {
        //PyR.RemoveAt(NumPregunta);

        OpcionSelecionada.SetActive(false);
        OpcionSelecionada.transform.parent = parentImagenSeleccion.transform;
        foreach (Transform child in ParentBoton.transform)
        {
            Destroy(child.gameObject);
        }
        GenerarPregunta();
    }

    void Respuestas() //Accedo a cada boton y a su texto correspondiente para cambiarlo por las posibles respuestas
    {
        for (int i = 0; i < jSONExamen.preguntas.PreguntasExamen[NumPregunta - 1].Respuestas.Count; i++)
        {
            GameObject prefabBoton = Instantiate(BotonRespuesta, ParentBoton.transform);
            //prefabBoton.transform.SetParent(ParentBoton.transform);
            prefabBoton.GetComponent<ExamenRespuestas>().Correcta = false;
            
            //Debug.Log(jSONExamen.preguntas.PreguntasExamen[NumPregunta].Respuestas);
            prefabBoton.GetComponentInChildren<TextMeshProUGUI>().text = jSONExamen.preguntas.PreguntasExamen[NumPregunta-1].Respuestas[i];


            if (jSONExamen.preguntas.PreguntasExamen[NumPregunta-1].RespuestaCorrecta == prefabBoton.GetComponentInChildren<TextMeshProUGUI>().text) //Si coincide el texto del boton con la respuesta correcta, cambiar la propiedad booleana para asi agregar puntaje
            {
                prefabBoton.GetComponent<ExamenRespuestas>().Correcta = true; 
            }
        }
    }

    public void GenerarPregunta() //Genero preguntas siempre y cuando haya preguntas que mostrar. Si se acaban las preguntas muestro panel de aprobado o reprobado dependiendo del puntaje
    {
        if(NumPregunta < jSONExamen.preguntas.PreguntasExamen.Length)
        {
            
            TextoPregunta.text = jSONExamen.preguntas.PreguntasExamen[NumPregunta].Pregunta;
            NumPregunta++;

            Respuestas();

        }
        else
        {
            PanelExamen.SetActive(false);
            SacarCalificacion();
            if (calificacion >= PuntajeAprobatorio)
            {
                
                TextoScoreAprobado.text = calificacion.ToString();
                PanelAprobado.SetActive(true);
                ResetScore();
            }
            else
            {
           
                TextoScoreReprobado.text = calificacion.ToString();
                PanelReprobado.SetActive(true);
                ResetScore();
            }
        }

    }

    public void BotonReintentar() //reiniciar la escena y el examen 
    {
        
        StoryTelling.ImageNum = 1;
        StoryTelling.UpdateUI();
        PanelReprobado.SetActive(false);
        PanelInstrucciones.SetActive(true);
        //PyR = PyR_Respaldo;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ResetScore() //restear el puntaje dado que es una variable estatica
    {
        Score = 0;
        NumPregunta = 1;
    }

   

    void SacarCalificacion() //Promedio del examen
    {
        Score = (Score/jSONExamen.preguntas.PreguntasExamen.Length) * 10;
        calificacion = Math.Round(Score, 2);
    }
}
