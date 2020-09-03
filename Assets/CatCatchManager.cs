using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCatchManager : MonoBehaviour
{
    public int catLaunchedAmount = 0;
    public int catCatched = 0;
    public int catLanded = 0;
    private bool hasEnded = false;
    private AudioSource _audio;
    public AudioClip WinSound;
    public AudioClip LoseSound;
    void Update()
    {
        if(catLanded >= catLaunchedAmount && !hasEnded)
        {
            hasEnded = true;
            int points = catCatched * 100;
            if (catCatched >= 3 * catLaunchedAmount / 4)
            {
                points += 1000;
                StartCoroutine("Win");
            }
            else
            {
                StartCoroutine("Lose");
            }
            Debug.Log(points);
            GameManager.Instance.AddScore(points);
            GameManager.Instance.LaunchTransition();
        }
    }

    public IEnumerable Win()
    {
        _audio.clip = WinSound;
        _audio.Play();
        yield return new WaitForSeconds(1.0f);
    }
    public IEnumerable Lose()
    {
        _audio.clip = LoseSound;
        _audio.Play();
        yield return new WaitForSeconds(1.0f);
    }

}
