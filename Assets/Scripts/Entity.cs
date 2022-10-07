using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYSTATES { IDLE, SEEK, SHOOT, FLEE, LONGSHOT, WANDER };
public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected BoxCollider2D bc2d;
    protected Health health;
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
    }

    protected void Start()
    {
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
