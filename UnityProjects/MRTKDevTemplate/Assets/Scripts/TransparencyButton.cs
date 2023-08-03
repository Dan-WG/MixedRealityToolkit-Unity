using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyButton : MonoBehaviour
{
    public ChangeTransparency [] TransparencyObjects;

    private void Start()
    {
        TransparencyObjects = FindObjectsOfType<ChangeTransparency>();
    }

    public void ButtonTransparency()
    {
        foreach(var obj in TransparencyObjects)
        {
            obj.ToggleTransparency();
        }

    }
}
