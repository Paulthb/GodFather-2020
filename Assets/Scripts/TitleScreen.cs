using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;
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
        if (player.GetButtonDown("ActionButton") || Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Start Game !");
            GameManager.Instance.StartGame();
        }
    }
}