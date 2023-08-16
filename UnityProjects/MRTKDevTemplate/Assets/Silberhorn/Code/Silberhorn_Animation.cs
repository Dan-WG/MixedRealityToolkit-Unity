using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Silberhorn_Animation : MonoBehaviour
{
    public Shader Base;
    public Shader Pulse;
    public Shader Xray;
    public List<Materiales> MatList = new List<Materiales>();
    
    // Start is called before the first frame update
    void Start()
    {
        foreach(Materiales mat in MatList)
        {
            mat.MainShader = mat.Mat.shader;
        }
    }

    public void XrayEffect()
    {
        foreach (Materiales mat in MatList)
        {
            mat.Mat.shader = Xray;
            mat.Mat.SetFloat("_Xray", 0);
        }

        foreach (Materiales mat in MatList)
        {
            DOVirtual.Float(0, 1, 2f, (float value) =>
            {
                mat.Mat.SetFloat("_Xray", value);
            });

        }
    }

    public void OffXray()
    {
        StartCoroutine(OffXrayIE());
    }

    public IEnumerator OffXrayIE()
    {

        foreach (Materiales mat in MatList)
        {
            if (mat.Mat.shader == Xray)
            {
                mat.Mat.SetFloat("_Xray", 1);
            }


            DOVirtual.Float(1, 0, 2f, (float value) =>
            {
                mat.Mat.SetFloat("_Xray", value);
            });

        }

        yield return new WaitForSeconds(2f);

        foreach (Materiales mat in MatList)
        {
            mat.Mat.shader = mat.MainShader;
        }


    }


    private void OnDisable()
    {
        foreach (Materiales mat in MatList)
        {
            mat.Mat.shader = mat.MainShader;
        }
    }

    [System.Serializable]
    public class Materiales
    {
        public Shader MainShader;
        public Material Mat;
    }

}
