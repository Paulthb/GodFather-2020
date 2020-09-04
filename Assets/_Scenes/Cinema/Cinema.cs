using System.Collections;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

public class Cinema : MonoBehaviour
{
    Player player;
    GameObject[] disps;
    GameObject[] guys;
    public GameObject hand;
    public Text text;
    public Image timerFill;
    public Sprite lol0;
    public Sprite lol1;
    public Sprite lol2;
    public Sprite lol3;
    Sprite newSprite;

    public AudioSource audioSource;
    public AudioClip audioClip0;
    public AudioClip audioClip1;

    bool gameWin;
    bool gameLose;
    float score;

    float timer;
    public float maxTimer;
    public int handSpeed;
    public int numberOfDisps;
    bool onGoodChair;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);

        int disp = Mathf.RoundToInt(Random.Range(0, numberOfDisps - 1));
        disps = GameObject.FindGameObjectsWithTag("disp");
        for (int i = 0; i < disps.Length; i++)
        {
            if (i != disp) disps[i].SetActive(false);
        }

        guys = GameObject.FindGameObjectsWithTag("guy");

        audioSource.PlayOneShot(audioClip0);
    }

    void Update()
    {
        if (!gameLose)
        {
            timer += Time.deltaTime;
            timerFill.fillAmount = (timer * .1F) / (maxTimer * .1F);
            if (timer < maxTimer)
            {
                float moveHorizontal = player.GetAxis("X_Axis");
                float moveVertical = player.GetAxis("Y_Axis");
                if (moveHorizontal != 0) hand.transform.Translate(Vector3.right * moveHorizontal * handSpeed * Time.deltaTime);
                if (moveVertical != 0) hand.transform.Translate(Vector3.up * moveVertical * handSpeed * Time.deltaTime);

                float debugHorizontal = Input.GetAxis("Horizontal");
                float debugVertical = Input.GetAxis("Vertical");
                if (debugHorizontal != 0) hand.transform.Translate(Vector3.right * debugHorizontal * handSpeed * Time.deltaTime);
                if (debugVertical != 0) hand.transform.Translate(Vector3.up * debugVertical * handSpeed * Time.deltaTime);

                if (player.GetButtonDown("ActionButton") || Input.GetKeyDown(KeyCode.E))
                {
                    if (onGoodChair) Win();
                    else Lose();
                }
            }
            else Lose();
        }
    }

    void Win()
    {
        score = maxTimer * 100 - Mathf.Floor(timer * 100);
        GameObject.Find("player").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("playerSit").GetComponent<SpriteRenderer>().enabled = true;
        gameWin = true;
        Time.timeScale = 0;
        text.enabled = true;
        text.text = "Good Job, Score: " + score.ToString();
        audioSource.Stop();
        SoundManager.Instance.StartWin();
        GameManager.Instance.AddScore(score);
        StartCoroutine(wait());
    }

    void Lose()
    {
        score = 0;
        gameLose = true;
        Time.timeScale = 0;
        foreach (GameObject guy in guys)
        {
            int lol = Mathf.RoundToInt(Random.Range(0, 3));
            if (lol == 0) newSprite = lol0;
            if (lol == 1) newSprite = lol1;
            if (lol == 2) newSprite = lol2;
            if (lol == 3) newSprite = lol3;
            guy.GetComponent<SpriteRenderer>().sprite = newSprite;
        }
        text.enabled = true;
        text.text = "Game Over, Score: " + score.ToString();
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip1);
        SoundManager.Instance.StartLoose();
        GameManager.Instance.AddScore(score);
        StartCoroutine(wait());
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.LaunchTransition();
    }

    void OnTriggerEnter(Collider c)
    {
        onGoodChair = true;
    }

    void OnTriggerExit(Collider c)
    {
        onGoodChair = false;
    }
}