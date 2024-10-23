using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private Player player;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Checkpoint checkpoint;
    [SerializeField] private GameObject winText;

    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        player.transform.position = playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (checkpoint.GetWincondition)
        {
            player.GetComponent<Animator>().SetBool("Win",true);
            DisableMovement();
            winText.SetActive(true);
        }
    }

    private void DisableMovement()
    {
        player.GetComponent<Mover>().enabled = false;
        player.GetComponent<ChangeGravity>().enabled = false;
    }
    public void ResetPlayerPosition(GameObject player)
    {
        player.transform.position = playerPosition;
    }
}
