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

    public bool transitionRunning = false;

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
        if (nextLevelString != "")
            LaunchNextGame();
        else
            LaunchHighScoreScene();
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
        if (SceneLeftInGame.Count > 0)
        {
            int randGameId = Random.Range(0, SceneLeftInGame.Count);

            Debug.Log(randGameId);

            if (SceneLeftInGame[randGameId] == "SnipeMasque")
            {
                nextLevelString = "SnipeMasque";
                nextLevelRule = "Trouve la personne sans masque";
            }

            else if (SceneLeftInGame[randGameId] == "PorteMamie")
            {
                nextLevelString = "PorteMamie";
                nextLevelRule = "Tiens la porte";
            }

            else if (SceneLeftInGame[randGameId] == "Matt")
            {
                nextLevelString = "Matt";
                nextLevelRule = "Souffle";
            }

            else if (SceneLeftInGame[randGameId] == "CatchCat")
            {
                nextLevelString = "CatchCat";
                nextLevelRule = "Rattrape les chats";
            }

            else if (SceneLeftInGame[randGameId] == "Cinema")
            {
                nextLevelString = "Cinema";
                nextLevelRule = "Respecte la distanciation sociale";
            }

            else if (SceneLeftInGame[randGameId] == "Fauteuil")
            {
                nextLevelString = "Fauteuil";
                nextLevelRule = "Aide mamie à traverser";
            }

            else if (SceneLeftInGame[randGameId] == "Plant")
            {
                nextLevelString = "Plant";
                nextLevelRule = "Arrose";
            }

            SceneLeftInGame.RemoveAt(randGameId);
        }

        else
        {
            Debug.Log("FIN DE RUN !");
            nextLevelString = "";
            nextLevelRule = "";
        }

        return nextLevelRule;
    }

    public void LaunchNextGame()
    {
        Debug.Log(nextLevelString);
        SceneManager.LoadScene(nextLevelString);


            //SceneLeftInGame.RemoveAt(randGameId);
        ////Quand tout les minis jeux sont fait dans la run
        //else if(!isRunFinnished)
        //{
        //    isRunFinnished = true;
        //    highScoreData.AddFinalScore(score);
        //    SceneManager.LoadScene("HighScoreScene");
        //    SoundManager.Instance.StartMainMenu();
        //}
    }

    public void LaunchHighScoreScene()
    {
        if (!isRunFinnished)
        {
            isRunFinnished = true;
            highScoreData.AddFinalScore(score);
            SceneManager.LoadScene("HighScoreScene");
            SoundManager.Instance.StartHighScore();
        }
    }

    public void BackToMainMenu()
    {
        SoundManager.Instance.StartMainMenu();
        SceneManager.LoadScene("MainMenu");
        SceneLeftInGame.AddRange(SceneName);
        isRunFinnished = false;
    }

    public void AddScore(float newPoints)
    {
        score += newPoints;
    }
}
