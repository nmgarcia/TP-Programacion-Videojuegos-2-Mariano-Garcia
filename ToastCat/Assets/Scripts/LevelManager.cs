using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] private List<GameObject> levels;
    [SerializeField] private LevelData currentLevelData;
    public LevelData GetCurrentLevelData => currentLevelData;
    
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
        switch (newState)
        {
            case GameStateEnum.LevelMenu:
            case GameStateEnum.MainMenu:
                DisableAllLevels();
                currentLevelIndex = 0;
                break;

            case GameStateEnum.ChangeLevel:                
                LoadNextLevel();
                break;

            case GameStateEnum.Playing:
                currentLevelData = levels.First(x => x.activeSelf).GetComponent<Level>().GetLevelData;
                currentLevelIndex = currentLevelData.Index;
                levels[currentLevelIndex].gameObject.SetActive(true);
               
                GameManager.Instance.SetPlayerSettings(currentLevelData);
                StateManager.Instance.ChangeState(GameStateEnum.Playing);
                break;
        }
    }

    private void DisableAllLevels()
    {
        foreach (var level in levels)        
            level.gameObject.SetActive(false);
    }

    public void EnableLevel(int levelIndex)
    {
        levels[levelIndex].gameObject.SetActive(true);
        //var level = levels[levelIndex].GetComponent<Level>().GetLevelData;
        //GameManager.Instance.SetPlayerSettings(level);
        //StateManager.Instance.ChangeState(GameStateEnum.Playing);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex == levels.Count)
        {
            StateManager.Instance.ChangeState(GameStateEnum.GameOver);
        }
        else
        {
            DisableAllLevels(); //Desactivamos todos los niveles para evitar errores
            EnableLevel(currentLevelIndex); //Habilitamos el siguiente index
        }
    }

    
}
