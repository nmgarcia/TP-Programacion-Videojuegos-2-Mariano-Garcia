using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ChangeGravity changeGravity;
    private Mover mover;

    private float moverHorizontal;
    private float moverVertical;

    private bool movementEnabled = true;

    private void OnEnable()
    {
        changeGravity = GetComponent<ChangeGravity>();
        mover = GetComponent<Mover>();
    }

    private void Update()
    {
        //Control de movimiento
        moverHorizontal = Input.GetAxis("Horizontal");
        moverVertical = Input.GetAxis("Vertical");
                
        mover.SetMovement(moverHorizontal, moverVertical);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            changeGravity.ResetGravity();
            GameManager.Instance.ResetPlayerPosition(gameObject);
        }
    }
}
