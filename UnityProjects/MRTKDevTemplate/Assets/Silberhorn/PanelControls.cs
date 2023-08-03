using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Microsoft.MixedReality.Toolkit.UX;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PanelControls : MonoBehaviour
{

    #region Variables
    public List<Texture2D> Imagenes;
    public List<Texture2D> CustomImagenes;
    public List<VideoClip> Videos;

    public GameObject PanelPDF, PanelImagenes, PanelVideos, ButtonNext, ButtonBack, ScaleText;
 
    
    public VideoPlayer PanelVideo;
    public RawImage rawImage;

    public static bool Pinned = false;
    int currentImg = 0, currentVideo = 0;
    public TextMeshProUGUI ImgNumber, ContadorTexto, Num;
    BoxCollider boxCollider;
    ObjectManipulator objectManipulator;
    //NearInteractionGrabbable interactionGrabbable;

    bool scaleEnabled;
    #endregion

    #region Unity Functions
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        objectManipulator = GetComponent<ObjectManipulator>();
        //interactionGrabbable = GetComponent<NearInteractionGrabbable>();
        rawImage.texture = Imagenes[0];
        PanelVideo.clip = Videos[0];
    }

    // Update is called once per frame
    void Update()
    {
        if(PanelImagenes.activeSelf)
        {
            ContadorTexto.text = "de " + Imagenes.Count;
            Num.text = (currentImg + 1).ToString();
            if(currentImg+1 == Imagenes.Count)
            {
                ButtonNext.SetActive(false);
            }
            else
            {
                ButtonNext.SetActive(true);
            }
            if (currentImg > 0)
            {
                ButtonBack.SetActive(true);
            }
            else
            {
                ButtonBack.SetActive(false);
            }
        }
        else if(PanelVideos.activeSelf)
        {
            ContadorTexto.text = "de " + Videos.Count;
            Num.text = (currentVideo + 1).ToString();
            if (currentVideo+1 == Videos.Count)
            {
                ButtonNext.SetActive(false);
            }
            else 
            {
                ButtonNext.SetActive(true);
            }
            if (currentVideo > 0)
            {
                ButtonBack.SetActive(true);
            }
            else
            {
                ButtonBack.SetActive(false);
            }
        }

        
    }
    #endregion

    #region Custom Functions
    public void ScaleEnable()
    {
        if (!scaleEnabled)
        {
            boxCollider.enabled = true;
            objectManipulator.enabled = true;
            //interactionGrabbable.enabled = true;
            scaleEnabled = true;
            ScaleText.SetActive(true);
        }
        else
        {
            boxCollider.enabled = false;
            objectManipulator.enabled = false;
            //interactionGrabbable.enabled = false;
            scaleEnabled = false;
            ScaleText.SetActive(false);
        }
    }

    public void NextButton()
    {
        if (PanelImagenes.activeSelf)
        {
            currentImg++;
        }
        else if (PanelVideos.activeSelf)
        {
            currentVideo++;
        }
        UpdateMedia();
    }

    public void PrevButton()
    {
        if (PanelImagenes.activeSelf)
        {
            currentImg--;
        }
        else if (PanelVideos.activeSelf)
        {
            currentVideo--;
        }
        UpdateMedia();
    }

    public void UpdateMedia()
    {
        rawImage.texture = Imagenes[currentImg];
        PanelVideo.clip = Videos[currentVideo];
    }

    public void PinnedFunction()
    {
        Pinned = !Pinned;
        if(Pinned == false)
        {
            this.gameObject.SetActive(false);
        }
    }

    #endregion

}
