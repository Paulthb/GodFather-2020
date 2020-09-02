using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
//using 

public class TitleScreen : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (player.GetButtonDown("ActionButton"))
        {
            Debug.Log("Start Game !"); 
        }
    }
}
