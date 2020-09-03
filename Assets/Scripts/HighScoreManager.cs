using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;

public class HighScoreManager : MonoBehaviour
{
    private Player player = null;

    [SerializeField]
    private HighScore highScoreData = null;
    [SerializeField]
    private Transform ScoresPoint = null;
    [SerializeField]
    private GameObject scorePrefab = null;
    [SerializeField]
    private Text yourScoreText = null;

    // Start is called before the first frame update
    void Start()
    {
        player = ReInput.players.GetPlayer(0);

        highScoreData.finalScoreList.Sort();
        highScoreData.finalScoreList.Reverse();

        ShowHighscore();
        ShowYourScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButtonDown("ActionButton"))
        {
            Debug.Log("Back to Main Menu");
            GameManager.Instance.BackToMainMenu();
        }
    }

    public void ShowYourScore()
    {
        yourScoreText.text = highScoreData.lastScore.ToString();
    }

    public void ShowHighscore()
    {
        int scoreId = 1;
        float scorePosY = 0;
        Vector3 scorePos;

        foreach(float highscore in highScoreData.finalScoreList)
        {
            scorePos = new Vector3(0, scorePosY, 0);
            GameObject scoreGE = Instantiate(scorePrefab, ScoresPoint);
            scoreGE.transform.localPosition = scorePos;
            scorePosY -= 100;

            scoreGE.GetComponent<Text>().text = scoreId + ".    <color=teal>" + highscore + "</color>";
            scoreId++;
        }
    }
}
