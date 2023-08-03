using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RotarYEscalar : MonoBehaviour
{
    private Vector3 OriginalScale, EscalaPanel; //Escalas de maquina y panel
    public Slider sliderEscala, sliderPanel; //sliders para panel y maquina
    public float rotateSpeed = 50f; //velocidad para rotar la maquina   
    private bool izq, derecha; //booleanas para girar
    public GameObject PanelAR; //Referencia al Panel

    private void Start()
    {
        OriginalScale = transform.localScale; //Sacar la escala inicial de la maquina
        //Establecer booleanas a falsas
        izq = false;
        derecha = false;
        EscalaPanel = PanelAR.transform.localScale; //Establecer escala de panel
    }

    private void Update()
    {
        if (izq) //Rotar izquiera si la booleana es verdadera
        {
            RotarIzq();
        }
        if (derecha)//Rotar derecha si la booleana es verdadera
        {
            RotarDerecha();
        }
    }

    public void Escalar() //Escalar maquina de acuerdo del valor del slider
    {
        transform.localScale = OriginalScale * (sliderEscala.SliderValue + .5f);
    }
    public void EscalarPanel()//Escalar panel de acuerdo del valor del slider
    {
        PanelAR.transform.localScale = EscalaPanel * (sliderPanel.SliderValue + .5f);
    }

    public void RotarIzqBoton()//funcion de boton para rotar izq
    {
        izq = true;
    }

    IEnumerator RotarDerecha()//rotar derecha
    {
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);
        return null;
    }
    IEnumerator RotarIzq() //rotar izq
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        return null;
    }
    public void RotarDerechaBoton()//funcion de boton para rotar derecha
    {
        derecha = true;
    }

    public void Stop()//detener rotacion
    {
        derecha = false;
        izq = false;
    }

  
}
