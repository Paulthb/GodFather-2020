﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class MoulinAVent : MonoBehaviour
{
    private Player player;
    public enum DIRECTION
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
        DEFAULT
    }

    public DIRECTION direction = DIRECTION.DEFAULT;
    public float rotationStrength = 0.0f;
    private void Start()
    {
        player = ReInput.players.GetPlayer(0);
    }
    void FixedUpdate()
    {
        Debug.Log(direction);
        if (rotationStrength < 0)
            rotationStrength = 0;

        switch (direction)
        {
            case DIRECTION.UP:
                if (player.GetAxis("X_Axis") < -0.1f)
                {
                    direction = DIRECTION.LEFT;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.RIGHT:
                if (player.GetAxis("Y_Axis") > 0.1f)
                {
                    direction = DIRECTION.UP;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.DOWN:
                if (player.GetAxis("X_Axis") > 0.1f)
                {
                    direction = DIRECTION.RIGHT;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.LEFT:
                if (player.GetAxis("Y_Axis") < -0.1f)
                {
                    direction = DIRECTION.DOWN;
                    rotationStrength += 0.33f;
                }
                break;
            case DIRECTION.DEFAULT:
                if (player.GetAxis("Y_Axis") > 0.1f)
                    direction = DIRECTION.UP;
                else if (player.GetAxis("X_Axis") < -0.1f)
                    direction = DIRECTION.LEFT;
                else if (player.GetAxis("Y_Axis") < -0.1f)
                    direction = DIRECTION.DOWN;
                else if (player.GetAxis("X_Axis") > 0.1f)
                    direction = DIRECTION.RIGHT;
                    break;
        }
    }
}
