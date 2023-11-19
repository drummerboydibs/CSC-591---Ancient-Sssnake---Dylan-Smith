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
    


    private void Start()
    {
        gameControllerObject = GameObject.Find("GameController");
        gameController = gameControllerObject.GetComponent<GameController>();
        player = GameObject.Find("Player");

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            gameController.CompleteLevel();
        }
    }

    

    //IEnumerator LoadLevel(int levelIndex)
    //{
    //    // Play animation
    //    //transition.SetTrigger("Start");

    //    // Load warp to next level start pos
    //    transportPlayerAndAllies(levelIndex);

    //    // Wait for animation to end
    //    yield return new WaitForSeconds(transitionTime);


    //}
}
