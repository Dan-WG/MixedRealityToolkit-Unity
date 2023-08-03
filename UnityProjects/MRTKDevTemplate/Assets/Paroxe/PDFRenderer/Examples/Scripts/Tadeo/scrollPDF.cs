using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrollPDF : MonoBehaviour
{
    public GameObject moveScrooll;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void moveUP()
    {

        moveScrooll.transform.localPosition= new Vector3(0,moveScrooll.transform.localPosition.y-800,0);
    }
    public void modeDown()
    {
        moveScrooll.transform.localPosition= new Vector3(0,moveScrooll.transform.localPosition.y+800,0);
    }
}
