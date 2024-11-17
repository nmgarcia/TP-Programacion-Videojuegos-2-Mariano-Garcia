using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    private bool isActive = false;
    [SerializeField] private Checkpoint checkpoint;
    public LevelData GetLevelData=>levelData;
    public Checkpoint GetCheckpoint =>checkpoint;
    public bool IsActive => isActive;

    private void OnEnable()
    {
        StateManager.Instance.ChangeState(GameStateEnum.Playing);
        isActive = true;
    }
    private void OnDisable()
    {
        isActive = false;
    }
}
