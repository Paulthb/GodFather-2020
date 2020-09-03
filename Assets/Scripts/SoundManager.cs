using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    private AudioSource source = null;

    [SerializeField]
    private AudioClip MainMenuSound = null;
    [SerializeField]
    private AudioClip MiniGameSound = null;
    [SerializeField]
    private AudioClip WinSound = null;
    [SerializeField]
    private AudioClip LooseSound = null;


    #region Singleton Pattern
    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void StartMainMenu()
    {
        source.clip = MainMenuSound;
        source.Play();
    }

    public void StartMiniGame()
    {
        source.clip = MiniGameSound;
        source.Play();
    }

    public void StartWin()
    {
        source.clip = WinSound;
        source.Play();
    }

    public void StartLoose()
    {
        source.clip = LooseSound;
        source.Play();
    }
}
