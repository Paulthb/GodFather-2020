using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCatchManager : MonoBehaviour
{
    public int catLaunchedAmount = 0;
    public int catCatched = 0;
    public int catLanded = 0;
    private bool hasEnded = false;
    public int points = 0;
    void Update()
    {
        if(catLanded >= catLaunchedAmount && !hasEnded)
        {
            hasEnded = true;
            points = catCatched * 100;
            if (catCatched >= 3 * catLaunchedAmount / 4)
            {
                points += 1000;
            }
            Debug.Log(points);
            GameManager.Instance.AddScore(points);
            GameManager.Instance.LaunchTransition();
        }
    }
}
