using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class PorteMamie : MonoBehaviour
{
    Player player;
    public GameObject door;
    public GameObject gentil;
    public GameObject mamie;
    public GameObject deathMamie;

    bool gameWin;
    bool gameLose;

    bool introDone = false;

    float timer;
    public float maxTimer;
    public bool timerText;

    [Range(0, 100)]
    public float initialForce;
    public float forceDecreasingSpeed;

    float score;

    public Image timerFill;
    public Image forceFill;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (timer < maxTimer && initialForce > 0)
        {
            timer += Time.deltaTime;
            initialForce -= Time.deltaTime * (timer + 5) * forceDecreasingSpeed;

            if (Input.GetKeyDown(KeyCode.Space) || player.GetButtonDown("ActionButton"))
            {
                initialForce += 5;
                score += 10;
            }
            if (initialForce < 0) Lose();
            if (initialForce > 100)
            {
                initialForce = 100;
                score += 50;
            }
        }
        else if (initialForce > 0) Win();
        Anim();
    }

    void Win()
    {
        gameWin = true;
    }

    void Lose()
    {
        gameLose = true;
        score = 0;
    }

    void Anim()
    {
        if (gameLose)
        {
            door.GetComponent<Animator>().Play("doorClose");
            door.SetActive(false);

            gentil.GetComponent<Animator>().Play("dropDoor");

            mamie.SetActive(false);
            deathMamie.SetActive(true);
        }
        if (gameWin)
        {
            gentil.GetComponent<Animator>().Play("happy");
        }
    }

    void OnGUI()
    {
        if (timerText) GUI.Box(new Rect((Screen.width - 100) / 2, 100, 100, 25), "TIMER: " + string.Format("{0:0.0}", timer));
        GUI.Box(new Rect((Screen.width - 200) / 2, 60, 200, 25), "SCORE: " + score);

        // GUI.DrawTexture(new Rect((Screen.width - 800) / 2, Screen.height - 60, 800, 20), Texture2D.grayTexture);
        // GUI.DrawTexture(new Rect((Screen.width - 800) / 2, Screen.height - 60, timer * (800 / maxTimer), 20), Texture2D.whiteTexture);
        timerFill.fillAmount = (timer * .1F) / (maxTimer * .1F);

        // GUI.DrawTexture(new Rect(Screen.width - 100, (Screen.height - 400) / 2, 20, 400), Texture2D.grayTexture);
        // GUI.DrawTexture(new Rect(Screen.width - 100, (Screen.height + 400) / 2, 20, -initialForce), Texture2D.whiteTexture);
        forceFill.fillAmount = initialForce * .01F;

        // if (gameWin) GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 25), "SUCCES");
        // if (gameLose) GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 25), "GAME OVER");
    }
}