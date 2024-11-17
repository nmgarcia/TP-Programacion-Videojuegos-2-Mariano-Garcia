using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class GameManager : MonoBehaviour
{
    private GameManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private ChangeGravity changeGravity;

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
        switch (newState) {
            case GameStateEnum.GameOver:                
                player.GetComponent<Animator>().SetBool("Win", true);
                DisableMovement();
                break;
        }
    }
    
    private void DisableMovement()
    {
        player.GetComponent<ChangeGravity>().enabled = false;
        player.GetComponent<Mover>().enabled = false;

        
    }
    public void ResetPlayerPosition(GameObject player)
    {
        player.transform.position = playerPosition;

    }

    public void SetPlayerSettings(LevelData levelData)
    {
        player.transform.localScale = new Vector3(levelData.PlayerScale, levelData.PlayerScale, 0);
        player.transform.position = playerPosition = levelData.PlayerPosition;
        changeGravity.ResetGravity();
    }
}
