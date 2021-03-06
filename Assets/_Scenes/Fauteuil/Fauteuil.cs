﻿using System.Collections;
using Rewired;
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
    public GameObject feuPieton;
    public Sprite feuPietonVert;
    public Sprite feuPietonRouge;
    public GameObject voiture0;
    public GameObject voiture1;

    public AudioSource audioSource;

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

    bool playAudio;

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

                GameObject.Find("audioDing").GetComponent<AudioSource>().enabled = true;
                feu.GetComponent<SpriteRenderer>().sprite = feuRouge;
                feuPieton.GetComponent<SpriteRenderer>().sprite = feuPietonVert;
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
        score = timeLeftToPlayer * 1000 - Mathf.Floor(-timer * 1000);
        gameWin = true;
        text.enabled = true;
        text.text = "Reaction Time: " + string.Format("{0:0.00}", -timer) + " seconds, Score: " + score;
        fauteuil.GetComponent<Animator>().Play("walk");
        StartCoroutine(wait());
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
        audioSource.Stop();
        SoundManager.Instance.StartLoose();
        GameManager.Instance.AddScore(score);
        StartCoroutine(waitwait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        feu.GetComponent<SpriteRenderer>().sprite = feuVert;
        feuPieton.GetComponent<SpriteRenderer>().sprite = feuPietonRouge;
        voiture1.GetComponent<SpriteRenderer>().enabled = true;
        voiture1.GetComponent<Animator>().Play("road1");
        voiture1.GetComponent<AudioSource>().enabled = true;
        GameObject.Find("audioVroum").GetComponent<AudioSource>().enabled = true;
        audioSource.Stop();
        SoundManager.Instance.StartWin();
        GameManager.Instance.AddScore(score);
        yield return new WaitForSeconds(2);
        GameManager.Instance.LaunchTransition();
    }

    IEnumerator waitwait()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.LaunchTransition();
    }
}