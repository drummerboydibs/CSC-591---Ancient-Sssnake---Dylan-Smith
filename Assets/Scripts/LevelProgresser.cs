using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class LevelProgresser : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    GameObject gameControllerObject;
    GameController gameController;
    GameObject player;
    public Vector3[] startPos;


    private void Start()
    {
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        player = GameObject.Find("Player");

        startPos[0] = new Vector3(0f, 18.9f, -13.28f);
        startPos[1] = new Vector3(0f, 18.9f, -13.28f);

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel((gameController.currentLevel)));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play animation
        transition.SetTrigger("Start");

        // Wait for animation to end
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        
    }

    public void transportPlayerAndAllies(int nextLevel)
    {
        player.transform.position = startPos[nextLevel];
        // spawn array of allies behind player at new levels
        // for temple level, they should spawn *above* player
    }
}
