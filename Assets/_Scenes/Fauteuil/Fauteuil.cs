﻿using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class Fauteuil : MonoBehaviour
{
    Player rewiredPlayer;

    public GameObject fauteuil;
    public GameObject feu;
    public Sprite feuVert;
    public Sprite feuOrange;
    public Sprite feuRouge;

    public Text text;
    public Image button;
    public Image fill0;
    public Image fill1;

    bool gameWin;
    bool gameLose;
    float score;

    float timer;
    public float timerMin;
    public float timerMax;
    public float timeLeftToPlayer;

    void Awake()
    {
        rewiredPlayer = ReInput.players.GetPlayer(0);

        timer = Mathf.RoundToInt(Random.Range(timerMin, timerMax));
    }

    void Update()
    {
        if (!gameWin && !gameLose)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                if (-timer >= timeLeftToPlayer) Lose();
                if (rewiredPlayer.GetButtonDown("ActionButton") || Input.GetKeyDown(KeyCode.E)) Win();

                feu.GetComponent<SpriteRenderer>().sprite = feuRouge;
                button.enabled = true;
                fill0.enabled = true;
                fill1.enabled = true;
                fill1.fillAmount = 1 - ((-timer * .1F) / (timeLeftToPlayer * .1F));
            }
            else if (rewiredPlayer.GetButtonDown("ActionButton") || Input.GetKeyDown(KeyCode.E)) Lose();
        }
    }

    void Win()
    {
        score = Mathf.Floor(-timer * 1000);
        gameWin = true;
        text.enabled = true;
        text.text = "Reaction Time: " + string.Format("{0:0.00}", -timer) + " seconds, Score: " + score;
        fauteuil.GetComponent<Animator>().Play("walk");
    }

    void Lose()
    {
        score = 0;
        gameLose = true;
        text.enabled = true;
        text.text = "Game Over, Score: " + score;
        GameObject.Find("Meuf").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Mec").GetComponent<SpriteRenderer>().enabled = true;
        fauteuil.GetComponent<SpriteRenderer>().enabled = false;
    }
}