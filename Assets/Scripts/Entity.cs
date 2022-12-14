using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ENEMYSTATES { IDLE, SEEK, SHOOT, FLEE, LONGSHOT, WANDER };


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected BoxCollider2D bc2d;
    [SerializeField] protected Health health;
    protected SpriteRenderer sr;
    [SerializeField] protected float speed = 2;
    protected Vector2 direction;
    [SerializeField] protected Transform bulletSpawn;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected GameObject missile;
    public bool isAlive = true;
    // Start is called before the first frame update
    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();        
        sr = GetComponent<SpriteRenderer>();
    }

    protected void Start()
    {
        rb2d.gravityScale = 0;
        //health = new Health(5, 5);
        print(health.CurrentHP());
    }

    protected void Shoot()
    {
        GameObject go = Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        go.GetComponent<Bullet>().SetDirection(direction);
        go.tag = this.tag;
    }

    //protected void LaunchMissile() 
    //{
    //    GameObject go = Instantiate(missile, bulletSpawn.position, Quaternion.identity);
    //    go.GetComponent<HomingMissile>().SetDirection(direction);
    //    go.tag = this.tag;
    //}

    public virtual void TakeDamage(int dmg) 
    {
        health.TakeDamage(dmg);
    }
}
