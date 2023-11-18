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
    public int currentLevel;
    

    public GameObject player;
    public PlayerController playerController;

    GameObject levelProgresserObject;
    LevelProgresser levelProgresser;

    private int maxLevel = 10;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
        currentLevel = 0;

        levelProgresserObject = GameObject.Find("LevelTrigger");
        levelProgresser = levelProgresserObject.GetComponent<LevelProgresser>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentHP == 0)
        {
            isGameOver = true;
        }
    }

    void levelUp(int year)
    {
        
        if (year == 0)
        {
            playerController.LevelUp();
        } else if (year > 0 && year <= maxLevel)
        {
            // increase the snakes' size and bite power
            year++;
            playerController.LevelUp();

        }
    }

    void completeLevel()
    {
        if (currentLevel < 2)
        {
            currentLevel++;
        }

        levelProgresser.transportPlayerAndAllies(currentLevel);
    }

    void addAlly(string allyName)
    {
        // add allies to an array
        // this will be checked
    }

}
