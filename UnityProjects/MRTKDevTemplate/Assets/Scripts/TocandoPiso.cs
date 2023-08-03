using Microsoft.MixedReality.Toolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TocandoPiso : MonoBehaviour
{

    public AudioSource audioSource;
    private bool tocando;
    public float PosY;
    bool once = false;

    private void Start()
    {
        //ActivarSpatialAwareness();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 31) //Por default la layer 31 es la de spatial awareness 
        {
            //Debug.Log("Tocando piso"); //Reproducir sonido al tocar psio
            tocando = true;
            PosY = transform.position.y;
            audioSource.Play();
        }
        else
        {
            tocando = false;
            //Debug.Log("Tocando algo" + collision.gameObject.name);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        tocando = false;
    }

    private void Update()
    {
        if (tocando)
        {
            //GetComponent<SolverHandler>().enabled = false;
            //GetComponent<TapToPlace>().StopPlacement();
            FreezePosY();
        }
       
    }

    void FreezePosY()
    {
        once = true;
        if (once)
        {
            transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
            once = false;
        }
        
    }
    /*public void DetenerSpatialAwareness() //Detener deteccion al posicionar maquina
    {
        CoreServices.SpatialAwarenessSystem.ResumeObservers();
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        var dataProviderAccess = observer as IMixedRealityDataProviderAccess;
        //Debug.Log(observer + "Desactivado");
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.None;
    }

    public void ActivarSpatialAwareness() //Reunadar la deteccion para posisionar maquina
    {
        CoreServices.SpatialAwarenessSystem.SuspendObservers();
        var observer = CoreServices.GetSpatialAwarenessSystemDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        var dataProviderAccess = observer as IMixedRealityDataProviderAccess;
        //Debug.Log(observer + "Activado");
        observer.Resume();
        observer.DisplayOption = SpatialAwarenessMeshDisplayOptions.Visible;
    }*/
}
