using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelector : MonoBehaviour
{
    public Button[] buttons = new Button[5];
    public ProgressData progressData;
    string dataPath = "test.json";
    private CustomSceneManager customSceneManager;

    private void Awake()
    {
        customSceneManager = GetComponent<CustomSceneManager>();
    }

    private void Start()
    {
        progressData = JsonUtility.FromJson<ProgressData>(SaveData.Load(dataPath));
        //buttons = FindObjectsOfType<Button>();
        for (int i = 0; i < progressData.levels.Length; i++)
        {
            if (progressData.levels[i] == 1)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }
}
