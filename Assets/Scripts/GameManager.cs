using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private int score;

    [SerializeField] Text scoreText;

    public ProgressData progress;

    string dataPath = "test.json";

    public int currentLevel;
    
    [SerializeField]private int levelScore;
    public int maxLevelScore = 100;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        progress = JsonUtility.FromJson<ProgressData>(SaveData.Load(dataPath));
        score = progress.totalScore;
        scoreText.text = score.ToString();
    }

    public void AddPoints(int points) 
    {
        score += points;
        levelScore += points;
        progress.totalScore = score;
        scoreText.text = score.ToString();
        if (levelScore >= maxLevelScore)
        {
            progress.levels[currentLevel] = 1;
        }

        string json = JsonUtility.ToJson(progress);
        SaveData.Save(dataPath, json);

        
    }
}
