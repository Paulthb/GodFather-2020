using System;
using System.Collections;
using System.Collections.Generic;
using Rewired;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class cameraMove : MonoBehaviour
{
    private Player player;

    private Transform cameraTransform = null;

    //for next juicy feature if needed
    private bool onValideTarget = false;
    private GameObject actualTarget = null;

    private bool isGameDone = false;

    [SerializeField]
    private float mainTimer = 20.0f;
    private float mainClock = 0.0f;

    private GameObject[] persoNMObjectTab;
    [SerializeField]
    private float cameraSpeed = 2;

    [SerializeField]
    private Sprite Perso1M_Sprite = null;
    [SerializeField]
    private Sprite Perso2M_Sprite = null;
    [SerializeField]
    private Image timerFillImage = null;

    [SerializeField]
    private float actualScore = 1200;

    [SerializeField]
    private GameObject masqueSnipePrefab = null;
    private GameObject masqueSnipeGAO = null;

    void Start()
    {
        player = ReInput.players.GetPlayer(0);
        cameraTransform = GetComponent<Transform>();
        persoNMObjectTab = GameObject.FindGameObjectsWithTag("NMPerso");
        //StartCoroutine(MasqueSnipeLifeTime());
    }

    void Update()
    {
        float moveHorizontal = player.GetAxis("X_Axis");
        float moveVertical = player.GetAxis("Y_Axis");

        if (moveHorizontal != 0)
            cameraTransform.Translate(Vector3.right * Time.deltaTime * moveHorizontal * cameraSpeed);

        if (moveVertical != 0)
            cameraTransform.Translate(Vector3.up * Time.deltaTime * moveVertical * cameraSpeed);

        if (player.GetButtonDown("ActionButton") && onValideTarget)
        {
            Debug.Log("shooted ?");
            ShootMask();
        }

        if (persoNMObjectTab.Length == 0 && !isGameDone)
        {
            //Victoire
            isGameDone = true;
            GameManager.Instance.AddScore(actualScore);
            GameManager.Instance.LaunchTransition();
            SoundManager.Instance.StartWin();
        }

        if(!GameManager.Instance.transitionRunning)
            mainClock += Time.deltaTime;
        if (mainClock >= mainTimer && !isGameDone)
        {
            //Défaite
            isGameDone = true;
            GameManager.Instance.LaunchTransition();
            SoundManager.Instance.StartLoose();
        }
        timerFillImage.fillAmount = mainClock / mainTimer;
        CalculateScore();

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NMPerso")
        {
            onValideTarget = true;
            Debug.Log("target validate");

            actualTarget = other.gameObject;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NMPerso")
            onValideTarget = false;
    }

    public void ShootMask()
    {
        Sprite oldSprite = actualTarget.GetComponent<SpriteRenderer>().sprite;
        if (actualTarget.tag != "MPerso")
        {
            //StartCoroutine(MasqueSnipeLifeTime());

            if (oldSprite.texture.name == "perso1_sm")
                actualTarget.GetComponent<SpriteRenderer>().sprite = Perso1M_Sprite;

            else if (oldSprite.texture.name == "perso2_sm")
                actualTarget.GetComponent<SpriteRenderer>().sprite = Perso2M_Sprite;

            actualTarget.tag = "MPerso";
        }
        persoNMObjectTab = GameObject.FindGameObjectsWithTag("NMPerso");
    }

    public IEnumerator MasqueSnipeLifeTime()
    {
        bool isMasqueSnipe = false;
        if (!isMasqueSnipe)
        {
            isMasqueSnipe = true;
            masqueSnipeGAO = Instantiate(masqueSnipePrefab, this.transform);
            //masqueSnipeGAO.transform.position = new Vector3(0, 0, 0.5f);
        }
        yield return new WaitForSeconds(1f);
        Destroy(masqueSnipeGAO);
    }

    public void CalculateScore()
    {
        actualScore = Mathf.Lerp(1200, 0, timerFillImage.fillAmount);
        //Debug.Log("actual score is : " + actualScore);
    }
}