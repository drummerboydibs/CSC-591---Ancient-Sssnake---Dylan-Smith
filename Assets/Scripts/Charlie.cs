using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Charlie : MonoBehaviour
{
    Dialogue dialogue;
    public FriendController friendController;

    // Start is called before the first frame update
    void Start()
    {
        dialogue = GetComponent<Dialogue>();
        friendController = GetComponent<FriendController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogue.sentences != null)
        {
            return;
        } else
        {
            Destroy(this);
        }
    }
}
