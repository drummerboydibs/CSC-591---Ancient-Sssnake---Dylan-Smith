using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int year = 0;
    public bool isGameOver = false;
    public int maxLevel = 10;

    GameObject player;
    PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentHP == 0)
        {
            isGameOver = true;
        }
    }

    void completeLevel(int year)
    {
        if (year == 0)
        {
            
        } else if (year > 0 && year <= maxLevel)
        {
            // increase the snakes' size and bite power
            year++;
            playerController.LevelUp();

        }
    }
}
