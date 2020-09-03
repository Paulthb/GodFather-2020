﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class CatchCat : MonoBehaviour
{
    public float playerSpeed = 1.0f;
    public int catCatched = 0;
    public CatLauncher launcher;
    private int catToCatch = 0;
    private Player player;
    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
        catToCatch = (3 * launcher.catAmount) / 4;
    }
    void Update()
    {
        Move();
    }
    public void Move()
    {
        float x = transform.position.x + player.GetAxis("X_Axis") * Time.deltaTime * playerSpeed;
        x = Mathf.Clamp(x, Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector3(1920, 0, 0)).x);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Cat"))
        {
            catCatched++;
            //if(catCatched >= catToCatch)
            //    //GameManager.Instance.LaunchTransition();
        }
    }
}