using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;


public class ProgressManager : MonoBehaviour
{
    [SerializeField] Text scoreText;

    private ProgressData progressData;
    private int score;
    string dataPath = "test.json";
    private void Start()
    {
        progressData = JsonUtility.FromJson<ProgressData>(SaveData.Load(dataPath));
        score = progressData.totalScore;
        scoreText.text = score.ToString();
    }

    public void ResetScore() 
    {
        progressData.totalScore = 0;
        score = 0;
        scoreText.text = score.ToString();
        string json = JsonUtility.ToJson(progressData);
        SaveData.Save(dataPath, json);
    }
}
