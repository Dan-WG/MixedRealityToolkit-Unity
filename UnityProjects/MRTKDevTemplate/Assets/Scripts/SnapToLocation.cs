
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnapToLocation : MonoBehaviour
{

    #region Variables


    [SerializeField] TextMesh AvisoSnap;

    [SerializeField]
    private Transform targetLocation;
    private bool isSnapped;
    [SerializeField]
    private float DistanceForSnap;

    public bool Grabbed;
    #endregion

    #region Unity Functions
    private void Update()
    {
        if (Grabbed)
        {
            CheckDistance();
        }

    }
    #endregion

    #region CustomFunctions

    public void Snap()
    {
        float Difference = Vector3.Distance(targetLocation.position, transform.position);
        //Debug.Log("Distancia: " + Difference);
        if(Difference < DistanceForSnap)
        {
            transform.position = targetLocation.position;
            transform.rotation = targetLocation.rotation;
            transform.localScale = targetLocation.localScale;
            isSnapped = true;
        }
        else
        {
            isSnapped = false;
        }
        if (isSnapped)
        {
            
            AvisoSnap.text = "Part placed correctly";
            AvisoSnap.color = Color.green;
            AvisoSnap.gameObject.SetActive(true);
            StartCoroutine(DisableText());
        }
        else
        {
            AvisoSnap.text = "Part out of place";
            AvisoSnap.color = Color.red;
            AvisoSnap.gameObject.SetActive(true);
            StartCoroutine(DisableText());
        }
    }

    public void CheckDistance()
    {
        float Difference = Vector3.Distance(targetLocation.position, transform.position);
        if (Difference < DistanceForSnap)
        {
            AvisoSnap.text = "Can let go of part to position it";
            AvisoSnap.color = Color.green;
            AvisoSnap.gameObject.SetActive(true);
        }
        else
        {
            AvisoSnap.gameObject.SetActive(false);
        }
    }

    IEnumerator DisableText() { 
        yield return new WaitForSeconds(2);
        AvisoSnap.gameObject.SetActive(false);
    }
    #endregion
}
