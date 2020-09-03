using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WateringCan : MonoBehaviour
{
    public float speed = 5.0f;
    private Player player;
    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        float x = player.GetAxis("X_Axis") * speed * Time.deltaTime;
        float y = player.GetAxis("Y_Axis") * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, 0);
    }
}

