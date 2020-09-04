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
    public GameObject backgroundMask;
    public Animator button;

    public AudioSource audioSource;
    public AudioClip audioClip0;
    public AudioClip audioClip1;

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
    public Text text;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);

        audioSource.PlayOneShot(audioClip0);
    }

    void Update()
    {
        if (timer < maxTimer && initialForce > 0)
        {
            timer += Time.deltaTime;
            initialForce -= Time.deltaTime * (timer + 5) * forceDecreasingSpeed;

            if (player.GetButtonDown("ActionButton") || Input.GetKeyDown(KeyCode.E))
            {
                initialForce += 5;
                score += 10;
            }
            if (initialForce < 0) StartCoroutine(Lose());
            if (initialForce > 100)
            {
                initialForce = 100;
                score += 50;
            }
        }
        else if (initialForce > 0) StartCoroutine(Win());
        Anim();
    }

    IEnumerator Win()
    {
        gameWin = true;
        button.enabled = false;
        audioSource.Stop();
        SoundManager.Instance.StartWin();
        GameManager.Instance.AddScore(score);
        yield return new WaitForSeconds(2);
        GameManager.Instance.LaunchTransition();
    }

    IEnumerator Lose()
    {
        gameLose = true;
        button.enabled = false;
        score = 0;
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip1);
        SoundManager.Instance.StartLoose();
        GameManager.Instance.AddScore(score);
        yield return new WaitForSeconds(2);
        GameManager.Instance.LaunchTransition();
    }

    void Anim()
    {
        if (gameLose)
        {
            door.SetActive(false);

            gentil.GetComponent<Animator>().Play("dropDoor");

            mamie.SetActive(false);
            deathMamie.SetActive(true);
            backgroundMask.SetActive(false);
        }
        if (gameWin)
        {
            door.GetComponent<Animator>().Play("doorClose");
            gentil.GetComponent<Animator>().Play("happy");
        }
    }

    void OnGUI()
    {
        if (timerText) GUI.Box(new Rect((Screen.width - 100) / 2, 100, 100, 25), "TIMER: " + string.Format("{0:0.0}", timer));
        text.text = score.ToString();

        timerFill.fillAmount = (timer * .1F) / (maxTimer * .1F);
        forceFill.fillAmount = initialForce * .01F;
    }
}