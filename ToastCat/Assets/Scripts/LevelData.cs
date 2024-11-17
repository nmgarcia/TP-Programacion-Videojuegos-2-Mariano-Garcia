using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewLevelData", menuName ="Level Data")]
public class LevelData : ScriptableObject
{
    [Header("Level Attributes")]
    public int Index;
    public string LevelName = "Level0";
    public bool IsLastLevel = false;

    [Header("Player Start Attributes")]    
    public float PlayerScale = 1.0f;
    public Vector2 PlayerPosition = Vector2.zero;
}
