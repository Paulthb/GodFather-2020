using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class WateringCan : MonoBehaviour
{
    public float speed = 5.0f;
    public float minY;
    private Player player;
    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        float x = transform.position.x + player.GetAxis("X_Axis") * speed * Time.deltaTime;
        float y = transform.position.y + player.GetAxis("Y_Axis") * speed * Time.deltaTime;
        x = Mathf.Clamp(x, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(1920, 0, 0)).x);
        y = Mathf.Clamp(y, minY, Camera.main.ScreenToWorldPoint(new Vector3(0, 1080, 0)).y);
        transform.position = new Vector3(x, y, 0);
    }
}

