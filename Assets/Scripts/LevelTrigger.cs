using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    BoxCollider2D bc2d;
    CustomSceneManager sceneHelper;

    public int nextLevel;

    private void Awake()
    {
        bc2d = GetComponent<BoxCollider2D>();
        sceneHelper = FindObjectOfType<CustomSceneManager>();
    }

    private void Start()
    {
        bc2d.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //sceneHelper.LoadScene("Level"+(GameManager.Instance.currentLevel+1).ToString());
            sceneHelper.LoadScene("Level"+(nextLevel).ToString());
        }
    }
}
