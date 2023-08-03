using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTransparency : MonoBehaviour
{
    [SerializeField]
    Material TransparentMaterial;

    Material OriginalMaterial;
    MeshRenderer renderer;
    bool IsTransparent;

    private void Awake()
    {
        
        renderer= GetComponent<MeshRenderer>();
        OriginalMaterial = renderer.material;
    }

    
    public void ToggleTransparency()
    {
        if (!IsTransparent)
        {
            renderer.material = TransparentMaterial;
            IsTransparent = true;
        }
        else
        {
            renderer.material = OriginalMaterial;
            IsTransparent = false;
        }
       
    }

}
