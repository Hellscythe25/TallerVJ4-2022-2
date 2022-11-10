using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] CircleCollider2D cc2d;
    Vector2 direction;
    private int bulletDamage = 1;
    private float lifeTime = 5f;
    private float speed = 4f;

    public void SetDirection(Vector2 dir) 
    {
        direction = dir;
    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        cc2d = GetComponent<CircleCollider2D>();
    }
    private void Start()
    {
        cc2d.isTrigger = true;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = direction * speed;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != this.gameObject.tag && collision.gameObject.layer != 2)
        {
            //DAMAGE
            collision.gameObject.GetComponent<Entity>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
