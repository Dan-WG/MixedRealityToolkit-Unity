using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertaControl : MonoBehaviour
{
    private bool Abierto; //booleana para checar si la puerta esta abierta o cerrada
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>(); //referencia al animator
    }


    public void Puerta()
    {
        if (Abierto) //cerrar puerta 
        {
            animator.Play("DoorAnimationClose");
            Abierto = false;
        }
        else //abrir puerta
        {
            animator.Play("DoorAnimation");
            Abierto = true;
        }
    }
}
