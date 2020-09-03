using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private Animator transitionAnime = null;

    //private bool isAnimationLaunched = false;

    #region Singleton Pattern
    private static TransitionManager _instance;

    public static TransitionManager Instance { get { return _instance; } }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        DontDestroyOnLoad(this.gameObject);

        if (!transitionAnime)
            Debug.Log("Il manque la transition");

    }

    public void StartTransition()
    {
        Debug.Log("animation launched");
        transitionAnime.SetBool("TransitionIn", true);
        transitionAnime.SetBool("TransitionOut", false);
        //transitionAnime.Play("Transition");

    }

    public void EndAnimation()
    {
        Debug.Log("animation ended");
        transitionAnime.SetBool("TransitionOut", true);
        transitionAnime.SetBool("TransitionIn", false);
        //transitionAnime.Play("Transition 1");
    }
}
