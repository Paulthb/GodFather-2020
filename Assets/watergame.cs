﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class watergame : MonoBehaviour
{
    public int waterAmount = 0;
    public int waterNeeded = 0;
    public float mainTimer = 20.0f;

    public Image timerFillImage;
    public SpriteRenderer Plant;
    public List<Sprite> plantStates;
    private int state = 0;

    private bool hasEnded = false;


    // Update is called once per frame
    void Update()
    {
        if(!hasEnded)
        {
            mainTimer -= Time.deltaTime;
            if (mainTimer <= 0)
            {
                //Défaite
                hasEnded = true;
                SoundManager.Instance.StartLoose();
            }
            else
            {
                switch (state)
                {
                    case 0:
                        if(waterAmount >= waterNeeded/3)
                        {
                            Plant.sprite = plantStates[0];
                            state = 1;
                        }
                        break;
                    case 1:
                        if (waterAmount >= 2*waterNeeded / 3)
                        {
                            Plant.sprite = plantStates[1];
                            state = 2;
                        }
                        break;
                    case 2:
                        if(waterAmount >= waterNeeded)
                        {
                            Plant.sprite = plantStates[2];
                        }
                        break;
                }
                if(waterAmount >= waterNeeded)
                {
                    //Victoire
                    hasEnded = true;
                    GameManager.Instance.AddScore((int)(mainTimer * 100));
                    SoundManager.Instance.StartLoose();
                    GameManager.Instance.LaunchTransition();
                }
            }
        }
        
    }
}
