using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Galeria : MonoBehaviour
{

    public List<Texture2D> Imagenes= new List<Texture2D>(); //Imagenes de la galeria (nuestra data)
    int[] dataIndexs = new int[0];
    private int relativeIndex = 0; //Contador de imágen de la galería

    public RawImage Imagen; //imagen principal
    public GameObject BackButtonGO, NextButtonGO; //botones de galeria

    private void Start() 
    {
        CheckStep();
    }

    private void OnEnable() {
        StoryTelling.stepUpdated += CheckStep;
        PanelAR_evento.panelEnabled += CheckStep;
    }
    private void OnDisable() {
        StoryTelling.stepUpdated -= CheckStep;
        PanelAR_evento.panelEnabled -= CheckStep;
    }

    /// Botones 
    public void NextButton() //Siguiente imágen 
    {
        relativeIndex++;
        Navigate();
    }

    public void BackButton() //Imágen anterior 
    {
        relativeIndex--;
        Navigate();
    }

    //Este método se usa para actualizar la galería al cambiar de paso
    private void CheckStep()
    {
        //Saber en qué paso vamos para obtener el array con los índices de la lista con los datos
        dataIndexs = JSONReader.diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum-1].imgIndexs;

        //Cada que pasamos a un nuevo paso volvemos a iniciar en la primera imágen de ese array
        relativeIndex = 0;

        //Si el paso no tiene imágenes desactivamos los botones y no mostramos nada
        if (dataIndexs.Length == 0)
        {
            NextButtonGO.SetActive(false);
            BackButtonGO.SetActive(false);
            Imagen.enabled = false;
        }
        else
        {
            Imagen.enabled = true;
            Navigate();
        }
        
    }

    //Este método se usa para navegar entre imágenes dentro del mismo paso
    void Navigate()
    {
        //ocultar o mostrar botones dependiendo si estoy en la primera o ultima imagen
        if(relativeIndex == 0)
        {
            BackButtonGO.SetActive(false);
        }
        else
        {
            BackButtonGO.SetActive(true);
        }

        if(relativeIndex < dataIndexs.Length - 1)
        {
            NextButtonGO.SetActive(true);
        }
        else
        {
            NextButtonGO.SetActive(false);
        }
        
        //Colocar la imágen correspondiente en el panel
        Imagen.texture = Imagenes[dataIndexs[relativeIndex]];
    }
}
