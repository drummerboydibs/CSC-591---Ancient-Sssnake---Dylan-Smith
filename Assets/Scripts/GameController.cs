using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public bool isGameOver = false;
    public int currentGameLevel = 0;
    public bool isTitleScreenActive = true;
    GameObject titleCam;
    GameObject mainCam;

    public GameObject player;
    
    public PlayerController playerController;
    GameObject soundLevel0;
    public GameObject soundLevel1;
    public GameObject soundLevel2;
    public GameObject interactUI;
    public int playerLevel = 1;
    GameObject titleScreen;
    GameObject gameOverScreen;
    

    GameObject levelProgresserObject;
    LevelProgresser levelProgresser;

    GameObject gameOverDialogue;

    
    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("TitleScreens");
        gameOverScreen = GameObject.Find("GameOverScreen");
        gameOverScreen.SetActive(false);
        titleCam = GameObject.Find("TitleCamera");
        titleCam.SetActive(true);
        player = GameObject.Find("Player");
        mainCam = GameObject.Find("Main Camera");
        mainCam.SetActive(false);
        playerController = player.GetComponent<PlayerController>();
        
                
        currentGameLevel = 0;

        levelProgresserObject = GameObject.Find("LevelTrigger");
        levelProgresser = levelProgresserObject.GetComponent<LevelProgresser>();

        interactUI = GameObject.Find("InteractMenu");
        interactUI.SetActive(false);
        
        soundLevel0 = GameObject.Find("DesertSound");
        soundLevel1 = GameObject.Find("TempleSound");
        soundLevel1.SetActive(false);
        soundLevel2 = GameObject.Find("PitSound");
        soundLevel2.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentHP == 0)
        {
            GameOver();
        }

        if (Input.GetKey(KeyCode.Keypad1))
        {
            TransportPlayerAndAllies(0);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            TransportPlayerAndAllies(1);
        }
        if (Input.GetKey(KeyCode.Keypad2))
        {
            TransportPlayerAndAllies(1);
        }
    }

    public void LevelUp()
    {
        // increase the snakes' size and bite power
        playerController.LevelUp();
        playerLevel++;
    }

    public void CompleteLevel()
    {
        if (currentGameLevel < 2)
        {
            currentGameLevel++;
        }

        TransportPlayerAndAllies(currentGameLevel);
        UpdateSoundscape(currentGameLevel);

    }

    //void AddAlly(string allyName)
    //{
    //    // add allies to an array
    //    // this will be checked 
    //}

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        isGameOver = true;
        gameOverScreen.SetActive(true);
    }

    // Restart game by reloading the scene
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void TransportPlayerAndAllies(int currentLevel)
    {
        if (currentLevel == 0)
        {
            player.transform.position = new Vector3(-1.02270f, 0.489f, -2.234f);
        } else if (currentLevel == 1)
        {
            player.transform.position = new Vector3(5.468f, 18.968f, 806.51f);
        } else if (currentLevel == 2)
        {
            player.transform.position = new Vector3(-1.66597f, 1.465f, 992.39f);
        }
    }

    void UpdateSoundscape(int currentLevel)
    {
        if (currentLevel == 0)
        {
            soundLevel0.SetActive(true);
            soundLevel1.SetActive(false);
            soundLevel2.SetActive(false);
            
        }
        else if (currentLevel == 1)
        {
            soundLevel0.SetActive(false);
            soundLevel1.SetActive(true);
            soundLevel2.SetActive(false);
            
        }
        else if (currentLevel == 2)
        {
            soundLevel0.SetActive(false);
            soundLevel1.SetActive(false);
            soundLevel2.SetActive(true);
            
        }
    }

    public void StartGame()
    {
        Destroy(titleScreen);

        // Continue button to trigger.
        // Array of canvas probably?
        
        mainCam.SetActive(true);
        GameObject.Find("TitleScreens").SetActive(false);
        isTitleScreenActive = false;
        return;
    }

}
