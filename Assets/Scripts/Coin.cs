using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;
    private int points;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        circleCollider.isTrigger = true;
        points = 20;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.AddPoints(points);
            this.gameObject.SetActive(false);
        }
    }
}
