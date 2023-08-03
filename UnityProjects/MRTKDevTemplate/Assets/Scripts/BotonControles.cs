using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class BotonControles : MonoBehaviour
{
    private bool ActiveVideo = false; //booleana para animador de Video
    private bool ActivePDF = false;//booleana para animador de PDF

    public Animator animatorVideo, animatorPDF; //animadores
    public GameObject PanelPDf, PanelVideo, PanelGaleria; //paneles
    public List<GameObject> GoDesactivar = new List<GameObject>(); //Demas objetos a desactivar

    private void OnEnable() {
        StoryTelling.stepUpdated += CloseVideo;

        //Seteamos el booleano en true para que desactive al inicio
        ActiveVideo = true;
        PanelAR_evento.panelEnabled += CloseVideo;
    }
    private void OnDisable() {
        StoryTelling.stepUpdated -= CloseVideo;
        PanelAR_evento.panelEnabled -= CloseVideo;
    }

    public void ActivarVideo() //Activar o desactivar panel de video 
    {
        if (!ActiveVideo)
        {
            PanelGaleria.SetActive(false);
            foreach (GameObject go in GoDesactivar)
            {
                go.SetActive(false);
            }
            PanelVideo.SetActive(true);
            animatorVideo.Play("VideoPanel");
            ActiveVideo = true;
        }
        else
        {
            CloseVideo();
        }
    }

    public void ActivarPDF() //Activar o desactivar panel de PDF
    {
        if (!ActivePDF)
        {

            PanelGaleria.SetActive(false);
            foreach (GameObject go in GoDesactivar)
            {
                go.SetActive(false);
            }
            PanelPDf.SetActive(true);
            animatorPDF.Play("PDFPanel");
            ActivePDF = true;
        }
        else
        {
            ClosePDF();
        }
    }
    IEnumerator EndofAnim(GameObject go) //Coorutina para activar demas objetos al cerrar PDF o Video
    {
        yield return new WaitForSeconds(1f);
        go.SetActive(false);
        foreach (GameObject Go in GoDesactivar)
        {
            if(JSONReader.diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum-1].Video)
            {
                Go.SetActive(true);
            }
        }
        PanelGaleria.SetActive(true);
    }

    void CloseVideo()
    {
        if(ActiveVideo)
        {
            animatorVideo.Play("VideoPanelReversa");
            ActiveVideo = false;
            StartCoroutine(EndofAnim(PanelVideo));
        }
    }
    void ClosePDF()
    {
        if (ActivePDF)
        {
            animatorPDF.Play("PDFPanelReversa");
            ActivePDF = false;
            StartCoroutine(EndofAnim(PanelPDf));
        }
    }
}
