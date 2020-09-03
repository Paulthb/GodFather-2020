using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private Animation transitionAnime = null;

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
        transitionAnime.Play("FirstAnimationIn");
        
    }

    public void EndAnimation()
    {

        Debug.Log("animation ended");
        transitionAnime.Play("FirstAnimationOut");
       
    }
}
