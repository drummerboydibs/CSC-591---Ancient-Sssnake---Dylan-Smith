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
    public int maxSnakes = 6;

    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void completeLevel(int year)
    {
        if (year == 0)
        {
            // load temple scene
            SceneManager.LoadScene("Level1_Temple");
            year++;
        } else if (year > 0 && year <= maxSnakes)
        {
            // increase the snakes' size and bite power


            year++;
        }
    }
}
