using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private int currentLevelIndex = 0;
    [SerializeField] private List<string> levels;
    [SerializeField] private LevelData currentLevelData;
    [SerializeField] private string currentlyLoadedScene;
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
                currentLevelIndex = 0;
                break;

            case GameStateEnum.ChangeLevel:
                LoadNextLevel();
                break;

            case GameStateEnum.Playing:
                LoadLevel(currentLevelIndex);
                break;
        }
    }

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count)
        {
            Debug.LogError($"Level index {levelIndex} is out of range!");
            return;
        }

        // Cargar la escena del nivel
        StartCoroutine(LoadLevelCoroutine(levels[levelIndex]));
        
    }

    private IEnumerator LoadLevelCoroutine(string sceneName)
    {
        yield return UnloadLevel();

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        // Esperar hasta que la escena esté completamente cargada
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        currentlyLoadedScene = sceneName;

        // Obtener datos del nivel actual (si hay un LevelData asociado)
        Level currentLevel = FindObjectOfType<Level>();
        if (currentLevel != null)
        {
            currentLevelData = currentLevel.GetLevelData;
            GameManager.Instance.SetPlayerSettings(currentLevelData);
        }

        // Cambiar al estado Playing
        StateManager.Instance.ChangeState(GameStateEnum.Playing);

    }

    public IEnumerator UnloadLevel()
    {
        if (!string.IsNullOrEmpty(currentlyLoadedScene) && levels.Contains(currentlyLoadedScene))
        {
            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(currentlyLoadedScene);
            while (!unloadOperation.isDone)
            {
                yield return null;
            }

            // Limpiar la referencia a la escena cargada
            currentlyLoadedScene = null;
        }
    }
    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levels.Count)
        {
            // Si no hay más niveles, cambiar a GameOver
            StateManager.Instance.ChangeState(GameStateEnum.GameOver);
        }
        else
        {
            SaveManager.Instance.SetInt(PlayerPrefsEnum.LevelReached, currentLevelIndex);
            SaveManager.Instance.Save();
            LoadLevel(currentLevelIndex);
        }
    }

    public void UnloadCurrentLevelFromUI()
    {
        StartCoroutine(UnloadLevel());
    }
}
