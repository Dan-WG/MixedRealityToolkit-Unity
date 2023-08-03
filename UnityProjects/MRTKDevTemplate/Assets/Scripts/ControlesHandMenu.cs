using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlesHandMenu : MonoBehaviour
{
    public Vector3 OriginalScale, EscalaReal; //escala original y escala a tamaño real
    public Slider sliderEscala; //slider para escalar modelo
    public GameObject SliderGO, Maquina, ButtonEscalaRealGO, ButtonEscalaAulaGO; //GOs de slider y la maquina a escalar
    public ThreeDModelFunctions modelFunctions; //Referencia al script de ThreeDModelFunctions
    bool Escala = false; //booleana para el tipo de escala, False = aula y True = tamaño real 

    private void Start()
    {
        OriginalScale = Maquina.transform.localScale;//guardar valor original del modelo de acuerdo a como este al iniciio
    }

    public void ButtonEscalaReal()//poner modelo en escala real y desactivar slider
    {
        Escala = true;
        modelFunctions.ResetModelParts();
        Maquina.transform.localScale = EscalaReal;
        SliderGO.SetActive(false);
    }

    public void CheckEscala()//funcion para checar en que escala esta despues de vista explosiva y activar botones correspondientes
    {
        if (!Escala)
        { 
            ButtonEscalaRealGO.SetActive(true);
        }
        else
        {
            ButtonEscalaAulaGO.SetActive(true);
        }
    }

    public void ButtonEscalaAula()//modelo a escala y activar slider
    {
        Escala = false;
        modelFunctions.ResetModelParts();
        Maquina.transform.localScale = OriginalScale;
        SliderGO.SetActive(true);
    }

    public void Escalar() //Escalar maquina de acuerdo del valor del slider
    {
        modelFunctions.ResetModelParts();
        Maquina.transform.localScale = OriginalScale * (sliderEscala.SliderValue + .5f);
    }


    public void ReloadScene()//Recargar escena 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
