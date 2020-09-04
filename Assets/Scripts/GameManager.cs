using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum GAMESTATE
    {
        TITLE,
        TRANSITION,
        INGAME,
        SCORE_SCREEN,
        END
    }
    GAMESTATE currentGameState = GAMESTATE.TITLE;

    public float score = 0;

    [SerializeField]
    private List<string> SceneName = null;
    private List<string> SceneLeftInGame = null;

    private bool animIn = false;
    private bool animOut = false;

    private bool transitionRunning = false;

    [SerializeField]
    private HighScore highScoreData = null;

    private bool isRunFinnished = false;
    private bool OnChangeLevel = false;

    private string nextLevelString = "";
    [SerializeField]
    private Text nextLevelText = null;

    #region Singleton Pattern
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        SceneLeftInGame = new List<string>();
        SceneLeftInGame.AddRange(SceneName);

        SoundManager.Instance.StartMainMenu();
    }

    public void StartGame()
    {
        LaunchTransition();
        SoundManager.Instance.StartMiniGame();
    }

    public void LaunchTransition()
    {
        if (!transitionRunning)
        {
            transitionRunning = true;
            StartCoroutine(TransitionCoroutine());
        }
    }

    public IEnumerator TransitionCoroutine()
    {
        //start transition
        yield return new WaitForSeconds(1f);
        TransitionManager.Instance.StartTransition();


        yield return new WaitForSeconds(1f);
        if (!OnChangeLevel)
            nextLevelText.text = NextLevelChoice();


        yield return new WaitForSeconds(1.5f);
        LaunchNextGame();
        nextLevelText.text = "";

        //end transition
        TransitionManager.Instance.EndAnimation();
        transitionRunning = false;

        OnChangeLevel = false;
    }

    public string NextLevelChoice()
    {
        OnChangeLevel = true;
        string nextLevelRule = "";
        int randGameId = Random.Range(0, SceneLeftInGame.Count);

        if (SceneLeftInGame[randGameId] == "SnipeMasque")
        {
            nextLevelString = "SnipeMasque";
            nextLevelRule = "Trouve la personne sans masque";
        }

        if (SceneLeftInGame[randGameId] == "PorteMamie")
        {
            nextLevelString = "PorteMamie";
            nextLevelRule = "Tiens la porte";
        }

        if (SceneLeftInGame[randGameId] == "Matt")
        {
            nextLevelString = "Matt";
            nextLevelRule = "Souffle";
        }

        if (SceneLeftInGame[randGameId] == "CatchCat")
        {
            nextLevelString = "CatchCat";
            nextLevelRule = "Rattrape les chats";
        }

        if (SceneLeftInGame[randGameId] == "Cinema")
        {
            nextLevelString = "Cinema";
            nextLevelRule = "Respecte la distanciation sociale";
        }

        if (SceneLeftInGame[randGameId] == "Fauteuil")
        {
            nextLevelString = "Fauteuil";
            nextLevelRule = "Aide mamie à traverser";
        }

        if (SceneLeftInGame[randGameId] == "Plant")
        {
            nextLevelString = "Plant";
            nextLevelRule = "Arrose";
        }

        SceneLeftInGame.RemoveAt(randGameId);
        return nextLevelRule;
    }

    public void LaunchNextGame()
    {
        if (SceneLeftInGame.Count != 0)
        {
            SceneManager.LoadScene(nextLevelString);
            //SceneLeftInGame.RemoveAt(randGameId);
        }
        //Quand tout les minis jeux sont fait dans la run
        else if(!isRunFinnished)
        {
            isRunFinnished = true;
            highScoreData.AddFinalScore(score);
            SceneManager.LoadScene("HighScoreScene");
            SoundManager.Instance.StartMainMenu();
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        SceneLeftInGame.AddRange(SceneName);
        isRunFinnished = false;
    }

    public void AddScore(float newPoints)
    {
        score += newPoints;
    }
}
