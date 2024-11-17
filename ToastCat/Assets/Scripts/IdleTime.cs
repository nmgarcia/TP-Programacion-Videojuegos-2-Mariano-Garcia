using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleTime : MonoBehaviour
{
    [SerializeField] private float idleTime = 20f; 
    [SerializeField] private Animator animator; 

    private float timeSinceLastInput;

    private void Update()
    {
        timeSinceLastInput += Time.deltaTime;

        if (CheckForInput())
        {
            timeSinceLastInput = 0f;
            animator.SetBool("IdleTime", false);
        }

        if (timeSinceLastInput >= idleTime)
        {
            PlayIdleAnimation();
        }
    }

    private bool CheckForInput()
    {
        return Input.anyKey ||
               Input.GetAxis("Horizontal") != 0 ||
               Input.GetAxis("Vertical") != 0;
    }

    private void PlayIdleAnimation()
    {        
        animator.SetBool("IdleTime",true);     
    }
}
