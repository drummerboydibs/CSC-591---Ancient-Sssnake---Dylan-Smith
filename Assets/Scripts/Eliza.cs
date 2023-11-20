using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Eliza : MonoBehaviour
{
    public GameObject barrelTrap;
    public FriendController friendController;

    // Start is called before the first frame update
    void Start()
    {
        barrelTrap = GameObject.Find("BarrelTrap");   
        friendController = GetComponent<FriendController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (barrelTrap != null)
        {
            return;
        } else
        {
            friendController.AddComponent<FriendController>();
            Destroy(this);
        }
            
        
    }
}
