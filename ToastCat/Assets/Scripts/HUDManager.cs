using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [SerializeField] private GameObject WinText;
    [SerializeField] private GameObject MainMenu;
    [SerializeField] private GameObject LevelMenu;
    [SerializeField] private GameObject GameUI;

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
    }

    private void OnEnable()
    {
        StateManager.Instance.OnStateChanged += HandleStateChange;
    }

    private void HandleStateChange(GameStateEnum newState)
    {
        WinText.SetActive(false);
        MainMenu.SetActive(false);
        LevelMenu.SetActive(false);
        GameUI.SetActive(false);

        switch (newState)
        {
            case GameStateEnum.MainMenu:
                MainMenu.SetActive(true);
                break;
            case GameStateEnum.Playing:
            case GameStateEnum.ChangeLevel:
                GameUI.SetActive(true);
                break;
            case GameStateEnum.LevelMenu:
                LevelMenu.SetActive(true);
                break;
            case GameStateEnum.GameOver:
                WinText.SetActive(true);
                GameUI.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (MainMenu.activeSelf)
            StateManager.Instance.ChangeState(GameStateEnum.MainMenu);

        if(LevelMenu.activeSelf)
            StateManager.Instance.ChangeState(GameStateEnum.LevelMenu);
    }
}
