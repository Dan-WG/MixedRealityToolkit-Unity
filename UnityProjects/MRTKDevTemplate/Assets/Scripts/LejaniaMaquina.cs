using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LejaniaMaquina : MonoBehaviour
{
    [SerializeField] 
    private GameObject Camera, PanelAdvertencia; //main camera y panel de advertencia 
    [SerializeField]
    private float distance, timer, cooldown; //distancia a la cual salta la advertencia y timer de cooldown para mostrar panel
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, Camera.transform.position) >= distance && timer >= cooldown) //activar panel de advertencia
        {
            PanelAdvertencia.SetActive(true);
        }
        else
        {
            PanelAdvertencia.SetActive(false); //desactivar panel si no se encuentra tan lejos
            timer += Time.deltaTime;
        }
    }

    public void Reposicionar() //En la funcion del boton de reposicion, agregar el start placement de tap to place
    {
        PanelAdvertencia.SetActive(false);
        timer = 0;
    }


}
