using Rewired;
using UnityEngine;

public class PorteMamie : MonoBehaviour
{
    Player player;

    bool gameWin;
    bool gameLose;

    float mainTimer = 5;
    float forcePorte = 100;

    // En attendant une variable globalScore à laquelle ajouter localScore.
    float localScore;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (forcePorte >= 0 && mainTimer < 10)
        {
            mainTimer += Time.deltaTime;
            forcePorte -= Time.deltaTime * mainTimer * 10;

            if (Input.GetKeyDown(KeyCode.Space) || player.GetButtonDown("ActionButton")) forcePorte += 20;
            if (forcePorte >= 400) Win();
        }
        else Lose();
    }

    void Win()
    {
        forcePorte = 400;
        localScore = Mathf.Round(10 - mainTimer) * 10;
        Time.timeScale = 0;
        gameWin = true;
    }

    void Lose()
    {
        mainTimer = 10;
        localScore = 0;
        Time.timeScale = 0;
        gameLose = true;
    }

    void OnGUI()
    {
        GUI.Box(new Rect((Screen.width - 100) / 2, (Screen.height - 600) / 2, 100, 25), "TIMER: " + string.Format("{0:0.0}", (10 - mainTimer)));

        GUI.DrawTexture(new Rect((Screen.width - 400) / 2, (Screen.height + 600) / 2, 400, 20), Texture2D.grayTexture);
        GUI.DrawTexture(new Rect((Screen.width - 400) / 2, (Screen.height + 600) / 2, forcePorte, 20), Texture2D.whiteTexture);

        if (gameWin) GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 25), "SUCCES");
        if (gameLose) GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height - 50) / 2, 200, 25), "GAME OVER");
        if (gameWin || gameLose) GUI.Box(new Rect((Screen.width - 200) / 2, (Screen.height + 50) / 2, 200, 25), "SCORE: " + localScore);
    }
}