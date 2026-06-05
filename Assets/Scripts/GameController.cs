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

    // Tracks all interactables currently in range of the player. UI is shown
    // iff this set is non-empty, so no single source can hide it on another's behalf.
    private readonly HashSet<Object> activeInteractSources = new HashSet<Object>();

    
    // Start is called before the first frame update
    void Start()
    {
        titleScreen = GameObject.Find("TitleScreens");
        gameOverScreen = GameObject.Find("GameOverScreen");
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
        titleCam = GameObject.Find("TitleCamera");
        if (titleCam != null) titleCam.SetActive(true);
        player = GameObject.Find("Player");
        mainCam = GameObject.Find("Main Camera");
        // If there's no title flow available, fall back to the main camera + active gameplay.
        if (titleScreen == null && titleCam == null)
        {
            isTitleScreenActive = false;
            if (mainCam != null) mainCam.SetActive(true);
        }
        else if (mainCam != null)
        {
            mainCam.SetActive(false);
        }
        if (player != null)
            playerController = player.GetComponent<PlayerController>();
        else
            Debug.LogError("GameController: 'Player' GameObject not found in scene.");


        currentGameLevel = 0;

        levelProgresserObject = GameObject.Find("LevelTrigger");
        if (levelProgresserObject != null)
        {
            levelProgresser = levelProgresserObject.GetComponent<LevelProgresser>();
        }

        interactUI = GameObject.Find("InteractMenu");
        if (interactUI != null) interactUI.SetActive(false);

        soundLevel0 = GameObject.Find("DesertSound");
        soundLevel1 = GameObject.Find("TempleSound");
        if (soundLevel1 != null) soundLevel1.SetActive(false);
        soundLevel2 = GameObject.Find("PitSound");
        if (soundLevel2 != null) soundLevel2.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerController != null && playerController.currentHP == 0)
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
        if (Input.GetKey(KeyCode.Keypad3))
        {
            TransportPlayerAndAllies(2);
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

    // Stop game, bring up game over text and restart button
    public void GameOver()
    {
        isGameOver = true;
        if (gameOverScreen != null) gameOverScreen.SetActive(true);
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
        if (soundLevel0 != null) soundLevel0.SetActive(currentLevel == 0);
        if (soundLevel1 != null) soundLevel1.SetActive(currentLevel == 1);
        if (soundLevel2 != null) soundLevel2.SetActive(currentLevel == 2);
    }

    public void RegisterInteractSource(Object src)
    {
        if (src == null) return;
        activeInteractSources.Add(src);
        if (interactUI != null) interactUI.SetActive(true);
    }

    public void UnregisterInteractSource(Object src)
    {
        if (src == null) return;
        activeInteractSources.Remove(src);
        if (interactUI != null && activeInteractSources.Count == 0)
        {
            interactUI.SetActive(false);
        }
    }

    public void StartGame()
    {
        if (titleScreen != null) Destroy(titleScreen);

        // Continue button to trigger.
        // Array of canvas probably?

        if (mainCam != null) mainCam.SetActive(true);
        var ts = GameObject.Find("TitleScreens");
        if (ts != null) ts.SetActive(false);
        isTitleScreenActive = false;
        return;
    }

}
