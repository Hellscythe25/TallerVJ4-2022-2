using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYSTATES { IDLE, SEEK, SHOOT, FLEE, LONGSHOT, WANDER };


[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected BoxCollider2D bc2d;
    protected Health health;
    protected SpriteRenderer sr;
    [SerializeField] protected float speed = 2;
    protected Vector2 direction;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected GameObject bullet;
    public bool isAlive = true;
    // Start is called before the first frame update
    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        health = GetComponent<Health>();
        sr = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        rb2d.gravityScale = 0;
        health.SetHealth(10);
        health.SetShields(20);
        //print(health.CurrentHP());
    }

    protected void Shoot()
    {
        GameObject go = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        go.GetComponent<Bullet>().SetDirection(direction);
        go.tag = this.tag;
    }

    public void TakeDamage(int dmg) 
    {
        health.TakeDamage(dmg);
    }
}
