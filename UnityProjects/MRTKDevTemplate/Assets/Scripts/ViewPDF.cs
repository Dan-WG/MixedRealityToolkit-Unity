using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPDF : MonoBehaviour
{

    public RawImage MainImage; //Imagen mostrando de PDF
    public List<Texture2D> paginas = new List<Texture2D>(); //Lista de imagenes de PDF
    //public List<GameObject> botones= new List<GameObject>();

   

    public void ChangePage(Texture2D pagina) //cambiar la imagen principal por la imagen dada en al funcion del boton
    {
        MainImage.texture = pagina; 
    }
}
