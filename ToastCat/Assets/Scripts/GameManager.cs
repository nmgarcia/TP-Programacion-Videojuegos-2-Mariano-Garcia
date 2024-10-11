using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameManager Instance;
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 playerPosition;
    [SerializeField] private Checkpoint checkpoint;
    [SerializeField] private GameObject winText;

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
        player.transform.position = playerPosition;
    }


    // Start is called before the first frame update
    void Start()
    {
        
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
        player.GetComponent<ChangeGravity>().enabled = false;
        player.GetComponent<Mover>().enabled = false;
    }
}
