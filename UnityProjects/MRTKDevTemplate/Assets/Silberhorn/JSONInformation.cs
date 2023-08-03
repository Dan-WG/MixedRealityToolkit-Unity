using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static JSONInformation;

public class JSONInformation : MonoBehaviour
{
    #region Variables
    public TextAsset textJSON;//archivo JSON

    public Root info = new Root();
    #endregion

    #region Unity Functions
    private void Awake()
    {
        info = JsonUtility.FromJson<Root>(textJSON.text); //Lectura de la info del JSON
    }

    private void Start()
    {
        Debug.Log(info.Detalles[0].Titulo);
    }
    #endregion

    #region Classes

    [System.Serializable]
    public class Informacion
    {
        public string Titulo;
        public string Descripcion;
        public List<VideoRA> _videoRA;
        public List<ImagenRA> _imagenRA;
        public List<Pdf> Pdf;
    }


    [System.Serializable]
    public class VideoRA
    {
        public int orden;
        public string NombreAsset;
        public string UrlServer;
        public string urlLocal;
        public string Tipo;
    }
    [System.Serializable]
    public class ImagenRA
    {
        public int orden;
        public string NombreAsset;
        public string UrlServer;
        public string urlLocal;
        public string Tipo;
    }
    [System.Serializable]
    public class Pdf
    {
        public int orden;
        public string titulo;
        public string UrlServer;
        public string urlLocal;
        public string tipo;
        public string NoPagina;
    }
   
  

    [System.Serializable]
    public class Root
    {
        public Informacion[] Detalles;
    }

    #endregion
}
