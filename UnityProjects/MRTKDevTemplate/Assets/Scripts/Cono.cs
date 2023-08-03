using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cono : MonoBehaviour
{
    
    public float snapDistance = 1; //distancia a la cual se posicionara en el lugar
    public List<Transform> nodes = new List<Transform>(); //lista de posibles posiciones donde colocar
    void Update()
    {
        
        float smallestDistance = snapDistance; 
        foreach (Transform node in nodes)
        {
            if (Vector3.Distance(node.position, transform.position) < smallestDistance) // si la distancia entre el objeto y un lugar de poscionamiento es menor a la distancia requerida, posicionarlo y emparentarlo
            {
                //Debug.Log("Snapping");
                transform.position = node.position;
                transform.parent = node.transform;
                smallestDistance = Vector3.Distance(node.position, transform.position);
            }
            
       
           
        }
        
    }

}
