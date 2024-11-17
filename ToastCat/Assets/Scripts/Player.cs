using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ChangeGravity changeGravity;
    private Mover mover;

    private float moverHorizontal;
    private float moverVertical;
    private bool playerMovementIsEnabled = true;

    private void OnEnable()
    {
        changeGravity = GetComponent<ChangeGravity>();
        mover = GetComponent<Mover>();
        StateManager.Instance.OnStateChanged += HandleStateChange;
        playerMovementIsEnabled = true;
    }

    private void HandleStateChange(GameStateEnum newState)
    {
        switch (newState)
        {
            case GameStateEnum.GameOver:
                DisableMovement();
                break;
        }
    }

    private void DisableMovement()
    {
        playerMovementIsEnabled = false;
        mover.SetMovement(0, 0);
        changeGravity.enabled = false;
    }

    private void Update()
    {
        if (playerMovementIsEnabled)
        {
            //Control de movimiento
            moverHorizontal = Input.GetAxis("Horizontal");
            moverVertical = Input.GetAxis("Vertical");
                
            mover.SetMovement(moverHorizontal, moverVertical);
        }
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
