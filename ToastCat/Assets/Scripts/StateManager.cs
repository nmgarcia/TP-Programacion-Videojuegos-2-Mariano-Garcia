using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance { get; private set; }

    [SerializeField] private GameStateEnum CurrentState;
    [SerializeField] private float ChangeStateDelay = 0.05f;

    public event Action<GameStateEnum> OnStateChanged;

    public GameStateEnum GetCurrentState=> CurrentState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        ChangeState(GameStateEnum.MainMenu);
    }

    public void ChangeState(GameStateEnum newState)
    {
        if (CurrentState != newState)
        {
            CurrentState = newState;
            StartCoroutine(DelayedStateChange(newState));
            HandleStateChange(newState);
        }
    }
    private IEnumerator DelayedStateChange(GameStateEnum newState)
    {
        //Nota: se estaban pisando algunos cambios de estado por lo cual puse un delay en el cambio para evitarlo
        yield return new WaitForSeconds(ChangeStateDelay);
        OnStateChanged?.Invoke(newState);
    }

    private void HandleStateChange(GameStateEnum newState)
    {
        switch (newState)
        {
            case GameStateEnum.MainMenu:
                Debug.Log("Main Menu State");
                break;

            case GameStateEnum.LevelMenu:
                Debug.Log("Level Menu State");
                break;
            case GameStateEnum.Playing:
                Debug.Log("Playing State");
                break;
            case GameStateEnum.ChangeLevel:
                Debug.Log("Change Level State");
                break;
            case GameStateEnum.GameOver:
                Debug.Log("Game Over State");
                break;
        }
    }
}
