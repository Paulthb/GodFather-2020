using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [SerializeField]
    private List<string> SceneName = null;

    private List<string> SceneLeftInGame = null;

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
        SceneLeftInGame = SceneName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        LaunchTransition();
    }

    public void LaunchTransition()
    {
        //start transition
        LaunchNextGame();
        //end transition
    }

    public void LaunchNextGame()
    {
        int randGameId = Random.Range(0, SceneLeftInGame.Count);
        SceneManager.LoadScene(SceneLeftInGame[randGameId]);
        SceneLeftInGame.RemoveAt(randGameId);
    }
}
