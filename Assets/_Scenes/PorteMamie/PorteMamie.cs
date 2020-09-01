using Rewired;
using UnityEngine;

public class PorteMamie : MonoBehaviour
{
    private Player player;

    bool gameWin;
    bool gameLose;

    float mainTimer = 5;
    float forcePorte = 100;

    void Awake()
    {
        player = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        if (forcePorte >= 0)
        {
            mainTimer += Time.deltaTime;
            forcePorte -= Time.deltaTime * mainTimer * 15;

            if (Input.GetKeyDown(KeyCode.Space) || player.GetButtonDown("ActionButton")) forcePorte += 20;
            if (forcePorte >= 400) Win();
        }
        else Lose();
    }

    void Win()
    {
        forcePorte = 400;
        Time.timeScale = 0;
        gameWin = true;
    }

    void Lose()
    {
        Time.timeScale = 0;
        gameLose = true;
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect((Screen.width - 400) / 2, (Screen.height + 600) / 2, 400, 20), Texture2D.grayTexture);
        GUI.DrawTexture(new Rect((Screen.width - 400) / 2, (Screen.height + 600) / 2, forcePorte, 20), Texture2D.whiteTexture);

        if (gameWin) GUI.Box(new Rect((Screen.width - 500) / 2, (Screen.height - 50) / 2, 500, 50), "SUCCES");
        if (gameLose) GUI.Box(new Rect((Screen.width - 500) / 2, (Screen.height - 50) / 2, 500, 50), "GAME OVER");
    }
}