using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;

public class StoryTelling : MonoBehaviour
{
    public delegate void UpdateStep();
    public static event UpdateStep stepUpdated;

    [SerializeField] private TextMeshProUGUI TextCounter, TextDiapositivas, warningText; //Texto contador de la interfaz7
    [SerializeField] private List<Transform> PanelPositions, LineRendererPositions; //Listas para posiciones del story telling
    [SerializeField] private List<GameObject> auras;
    [SerializeField] private LineRenderer lineRenderer;
    public GameObject Instrucciones;
    public static GameObject PanelLinePos;
    public DirectionalIndicator directionalIndicator;

    [SerializeField] private GameObject PanelGo, PanelWarning;
    public static int ImageNum = 1; //contador de en que paso vamos

    [SerializeField] private GameObject NextButtonGo, BackButonGo, BotonIniciarExamen, videoButtonGo; //botones de la UI

    private void OnEnable() {
        PanelAR_evento.panelEnabled += UpdateUI;
    }
    private void OnDisable() {
        PanelAR_evento.panelEnabled -= UpdateUI;
    }
    void Start()
    {
        ImageNum = 1; //Darle valor de 1, dado que empezamos siempre en la primera instruccion
        UpdateUI();
        Debug.Log("Diapositivas: " + JSONReader.diapositivas.PuntosDeInspeccion.Length);
        TextDiapositivas.text = JSONReader.diapositivas.PuntosDeInspeccion.Length.ToString();
    }

    void Update()
    {
        if(PanelLinePos == null && PanelGo.activeInHierarchy)
        {
            PanelLinePos = GameObject.FindGameObjectWithTag("LinePanelPosition");     
        }
        if (PanelLinePos != null)
        {
            lineRenderer.SetPosition(0, PanelLinePos.transform.position);
        }
    }


    public void UpdateUI() //Actualizacion de la interfaz
    {
        //Activar un evento para notificar a otras clases que la UI se está actualizando
        //O lo que es lo mismo, que estamos cambiando de diapositiva
        if (stepUpdated != null)
        {
            stepUpdated();
        }
        
        if (ImageNum == JSONReader.diapositivas.PuntosDeInspeccion.Length) //Si estamos en la ultima instruccion, desactivar el boton de siguiente y mostrar boton para iniciar examen
        {
            BotonIniciarExamen.SetActive(true);
            NextButtonGo.SetActive(false);
        }
        else if (ImageNum == 1) //Desactivar boton de anterior si estamos en la primera diapositiva
        {
            BackButonGo.SetActive(false);
            //PanelTitulo.text = "PASO 1";
        }

        if(ImageNum > 1) //Activar boton de anterior si estamos en alguna instrucciones igual o mayor a la segunda
        {
            BackButonGo.SetActive(true);
        }
        if (ImageNum < JSONReader.diapositivas.PuntosDeInspeccion.Length) //Volver a activar el boton de siguiente y desactivar boton de examen si no estamos en la ultima instruccion
        {
            BotonIniciarExamen.SetActive(false);
            NextButtonGo.SetActive(true);
        }

        //Revisar si el paso en el que vamos no tiene video
        if (JSONReader.diapositivas.PuntosDeInspeccion[ImageNum-1].Video == false)
        {
            videoButtonGo.SetActive(false);
        }
        else
        {
            videoButtonGo.SetActive(true);
        }

        TextCounter.text = ImageNum.ToString();
        lineRenderer.SetPosition(1, LineRendererPositions[ImageNum-1].position);
        directionalIndicator.DirectionalTarget = LineRendererPositions[ImageNum - 1].transform;
        PanelGo.transform.position = PanelPositions[ImageNum - 1].position;

        ActivateAura();
        CheckForWarning();
    }

    void ActivateAura()
    {
        //Tomar las otras auras y desactivarlas
        foreach (GameObject aura in auras)
        {
            aura.SetActive(false);
        }

        //Activar aura
        auras[ImageNum-1].SetActive(true);
    }

    void CheckForWarning()
    {
        //Revisar si el paso actual tiene advertencias
        if (JSONReader.diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum-1].WarnigsRA.Count > 0)
        {
            //Activar el panel con la advertencia
            PanelWarning.SetActive(true);
            warningText.text = JSONReader.diapositivas.PuntosDeInspeccion[StoryTelling.ImageNum-1].WarnigsRA[0].contenido;
        }
        else
        {
            //Desactivar el panel con la advertecia
            PanelWarning.SetActive(false);
        }
        
    }

    public void NextButton() //Siguiente imagen del story telling
    {
        ImageNum++;
        UpdateUI();
    }


    public void BackButton() //Imagen anterior del story telling
    {
        ImageNum--;
        UpdateUI();
    }

}
