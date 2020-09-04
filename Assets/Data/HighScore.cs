using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HighScore", menuName = "HighScore")]
public class HighScore : ScriptableObject
{
    public List<float> finalScoreList = new List<float>();

    public int lastScore = 0;

    public void AddFinalScore(float newScore)
    {
        lastScore = (int)newScore;
        finalScoreList.Add(newScore);
        Debug.Log("liste non classé : ");
    }
}
