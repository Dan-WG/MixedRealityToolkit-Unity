using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjustarColliders : MonoBehaviour
{

    public BoxCollider resizeCollider;

    
    // Start is called before the first frame update
    void Start()
    {
        ///AUN NO SIRVE ESTE SCRIPT 
    }

    // Update is called once per frame
    void Update()
    {
        resizeCollider.size= new Vector3(40f, 40f, 30f);
    }
}
