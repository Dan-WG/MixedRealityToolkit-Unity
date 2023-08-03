using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePanelRotate : MonoBehaviour
{

    public GameObject Camera; //main camera
    public float distance; //distancia a la cual se desactiva al rotacion
    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(gameObject.transform.position, Camera.transform.position) <= distance) //desactivar rotacion si el usuario esta cerca
        {
            this.gameObject.GetComponent<rotation_panel>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<rotation_panel>().enabled = true; //activar rotacion si el usuario se encuentra lejos
        }

        //Debug.Log(Vector3.Distance(gameObject.transform.position, Camera.transform.position));
    }
}
