using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angus : MonoBehaviour
{
    GameObject shadyMan;
    // Start is called before the first frame update
    void Start()
    {
        shadyMan = GameObject.Find("ShadyMan");

    }

    // Update is called once per frame
    void Update()
    {
        if (shadyMan != null)
        {
            return;
        } else
        {
            gameObject.AddComponent<FriendController>();
            Destroy(this);
        }
    }
}
