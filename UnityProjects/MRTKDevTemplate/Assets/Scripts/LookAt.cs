using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform camera;
    void Start()
    {
        camera = GameObject.Find("Main Camera").transform;
    }

    
    void Update()
    {
        transform.LookAt(camera);
    }
}
