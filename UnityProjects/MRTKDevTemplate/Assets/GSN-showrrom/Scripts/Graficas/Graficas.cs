//using Microsoft.MixedReality.Toolkit.Utilities.FigmaImporter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]//esta parte es para mi objeto sirva en el entorno grafico de unity
public class Grafica
{
    public string NombreGrafica;//esta variable es simbolica para que sepan cual es el nombre de la grafica
    public Text TextUiNombreGrafica;
    public Text textUINumero;
    public Text textInfoAlert;
    public int valoMaximoGrafica;
    public int RangoCriticoMax;
    public int RangoCriticoMin;
    public int RangoDeAbvertenciaMax;
    public int RangoDeAbvertenciaMin;
    
    public int RangoEstableMax;
    public int RangoEstableMin;
    public Image grafica;
    public Color ColorCritico;
    public Color ColorAbvertencia;
    public Color ColorEstable;

}
public class Graficas : MonoBehaviour
{
    public Grafica[] datosGraficas;
    bool panelActivated;
    
    private void OnEnable() 
    {
        panelActivated = true;
    }

    private void OnDisable() 
    {
        panelActivated = false;
    }

    void Update()
    {
        //Solo si el panel está activado actualizamos los datos
        for (int i = 0; i < datosGraficas.Length && panelActivated; i++)
        {
            //Asignamos el nombre de la gráfica en la UI
            datosGraficas[i].TextUiNombreGrafica.text=datosGraficas[i].NombreGrafica;

            // en el substring vamos a obtner el valor completo de string ejemplo= "10%" y el weservicePaginaWb.... nos servira para obtener el final del caracte ejemplo= "%" y cortar sin importar el numero de string ejemplo="pza/h"
            if( datosGraficas[i].NombreGrafica =="OEE" && WebServicesPaginaWeb.valorActual.Length != 0)//OEE
            {
                AdjustGraphic(WebServicesPaginaWeb.valorActual[0], i);
            }

            if(datosGraficas[i].NombreGrafica=="TEMPERATURA" && WebServicesPaginaWeb.valorActual.Length != 0)//TEMPERATURA
            {
                AdjustGraphic(WebServicesPaginaWeb.valorActual[1], i);
            }

            if(datosGraficas[i].NombreGrafica=="PRESIÓN" && WebServicesPaginaWeb.valorActual.Length != 0)//PORESION
            {
                AdjustGraphic(WebServicesPaginaWeb.valorActual[2], i);
            }

            if(datosGraficas[i].NombreGrafica=="CONTEO DE PIEZAS" && WebServicesPaginaWeb.valorActual.Length != 0)//CONTEO DE PIEZAS
            {
                AdjustGraphic(WebServicesPaginaWeb.valorActual[3], i);
            }

            if(datosGraficas[i].NombreGrafica=="NUMEROACCESO")//CONTEO DE PIEZAS
            {
                float valorConvertido = float.Parse(datosGraficas[i].textUINumero.text);
                AdjustGraphic(valorConvertido, i);
            }
        }
    }

    private void AdjustGraphic(float valorConvertido, int i)
    {
        //Rellenamos el color según lo cerca que esté el valor del límite
        datosGraficas[i].grafica.fillAmount = valorConvertido / datosGraficas[i].valoMaximoGrafica * 1; 
            
        //se activan funciones dependiendo del valor actual
        if(valorConvertido <= datosGraficas[i].RangoCriticoMax && valorConvertido >= datosGraficas[i].RangoCriticoMin)
        {
            FunctionCritico();
        }
        else if(valorConvertido <= datosGraficas[i].RangoDeAbvertenciaMax && valorConvertido >= datosGraficas[i].RangoDeAbvertenciaMin)
        {
            FunctionAbvertencia();
        }
        else if(valorConvertido <= datosGraficas[i].RangoEstableMax && valorConvertido >= datosGraficas[i].RangoEstableMin)
        {
            FunctionEstable();
        }
    }

    private void FunctionCritico()
    {
        for (int i = 0; i < datosGraficas.Length; i++)
        {
            datosGraficas[i].grafica.color= datosGraficas[i].ColorCritico;
            datosGraficas[i].textInfoAlert.text="ALERTA";
        }
        
    }
    private void FunctionAbvertencia()
    {
        for (int i = 0; i < datosGraficas.Length; i++)
        {
            datosGraficas[i].grafica.color= datosGraficas[i].ColorAbvertencia;
            datosGraficas[i].textInfoAlert.text="ADVERTENCIA";
        }
        
    }
    private void FunctionEstable()
    {
        for (int i = 0; i < datosGraficas.Length; i++)
        {
            datosGraficas[i].grafica.color= datosGraficas[i].ColorEstable;
            datosGraficas[i].textInfoAlert.text="ESTABLE";
        }
        
    }
    
}
