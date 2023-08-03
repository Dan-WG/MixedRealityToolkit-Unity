using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static JSONReader;

public class JSONReader : MonoBehaviour
{
    public TextAsset textJSON; //Archivo JSON

    public StoryTelling storyTelling; //Storytelling para acceder a instrucciones

    //Suscribimos esta clase al evento que se activa cuando cambiamos de diapositiva
    //Al cambiar de diapositiva actualiza los textos
    private void OnEnable() {
        StoryTelling.stepUpdated += UpdateSlide;
        diapositivas = JsonUtility.FromJson<Diapositivas>(textJSON.text); //Lectura de la info del JSON
    }
    private void OnDisable() {
        StoryTelling.stepUpdated -= UpdateSlide;
    }

    [System.Serializable]
    public class Instruccion //Estructura de datos de JSON
    {
        public string Titulo;
        public string Descripcion;
        public string MaxParametros;
        public bool StatusMaxParametros;
        public string NombreChecklist;
        public bool Video;
        public int[] VideoIndex;
        public bool Imagenes;
        public int[] imgIndexs;
        public List<Warnigs> WarnigsRA;
    }

    [System.Serializable]
    public class Warnigs
    {
        public int orden;
        public string titulo;
        public string contenido;
    }

    [System.Serializable]
    public class Diapositivas
    {
        public Instruccion[] PuntosDeInspeccion; //
    }

    public static Diapositivas diapositivas = new Diapositivas(); 
    //public TextMeshPro TituloPanel; //Variable para cambiar el texto de titulo

    /* private void Awake() 
    {
        diapositivas = JsonUtility.FromJson<Diapositivas>(textJSON.text); //Lectura de la info del JSON
    } */

    private void UpdateSlide()
    {
        //TituloPanel.text = diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum-1].Titulo; //Cambio de texto de titulo de acuerdo a la diapositiva
        storyTelling.Instrucciones.GetComponent<TextMeshProUGUI>().text = diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum - 1].Descripcion; //Cambio de texto en el panel de acuerdo a el paso en el cual vayamos
    }
}
