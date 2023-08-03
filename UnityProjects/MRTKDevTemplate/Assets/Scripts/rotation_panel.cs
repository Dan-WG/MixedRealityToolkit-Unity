using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_panel : MonoBehaviour
{

    public GameObject OBJcamera; //main camera
    //public GameObject NameCamera;

    private Vector3 angulos;
 

    // Update is called once per frame
    void Update()
    {

   
        angulos = OBJcamera.GetComponent<Transform>().rotation.eulerAngles; //rotar panel a que siempre vea la camara 
        //   Debug.Log(OBJcamera.GetComponent<Transform>().rotation.eulerAngles);

        angulos.z = 0;//en el eje z no queremos modiciar
  
        transform.rotation = Quaternion.Euler(angulos); //aplicar rotacion
        //        Debug.Log(angulos);
    }
}
