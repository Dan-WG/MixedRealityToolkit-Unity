using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOT_Viewer : MonoBehaviour
{
    [SerializeField] GameObject IOT;
    public bool canToggle = true;
    private Animator IOT_Anim;
    private void Start() 
    {
        //Take the animator component of the IOT
        IOT_Anim = IOT.GetComponent<Animator>();
    }
    public void ToggleIOT()
    {
        if(canToggle)
        {
            canToggle = false;

            //Enable and disable the IOT panels
            if(IOT.activeInHierarchy)
            {
                //Perform the vanish animation of the IOT
                StartCoroutine(VanishIOT());
            }
            else
            {
                StartCoroutine(AppearIOT());
            }

        }   
    }

    IEnumerator VanishIOT()
    {
        IOT_Anim.SetTrigger("Disable");
        //After finshing the animation disable the IOT
        yield return new WaitForSeconds(4);
        IOT.SetActive(false);
        canToggle = true;
    }

    IEnumerator AppearIOT()
    {
        IOT.SetActive(true);
        //After finshing the animation disable the IOT
        yield return new WaitForSeconds(4);
        canToggle = true;
    }
}
