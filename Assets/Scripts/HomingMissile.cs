using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.SteeringBehaviours;

public enum MISSILESTATES { STARING, SEEK};
public class HomingMissile : MonoBehaviour
{
    public MISSILESTATES states;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] int damage;
    [SerializeField] Vector2 direction;
    [SerializeField] float lifeTime;

    [SerializeField] ParticleSystem ps;
    [SerializeField] float angle;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] BoxCollider2D bc2d;
    [SerializeField] bool isPlaying = false;
    [SerializeField] float destroyTime;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        ps = GetComponentInChildren<ParticleSystem>();
    }
    private void Start()
    {
        states = MISSILESTATES.STARING;
        bc2d.isTrigger = true;
    }
    public void Initialize(int dmg, Transform t, float a) 
    {
        target = t;
        damage = dmg;
        angle = a;
    }

    private void FixedUpdate()
    {
        lifeTime += Time.fixedDeltaTime;

        if (lifeTime > 1f)
        {
            states = MISSILESTATES.SEEK;
            if (!isPlaying)
            {
                ps.Play();
                isPlaying = true;
            }            
        }

        if (lifeTime >= 30)
        {
            rotateSpeed = 200;
        }

        if (target != null)
        {
            direction = (Vector2)target.position - (Vector2)this.transform.position;
            direction.Normalize();
            switch (states) 
            {
                case MISSILESTATES.STARING:
                    rb2d.velocity = SteeringBehaviours.RotateVector(direction, angle) * speed / 3;
                    float rotateAmount = Vector3.Cross(direction, transform.up).z;
                    rb2d.angularVelocity = -rotateAmount * rotateSpeed;
                    break;
                case MISSILESTATES.SEEK:
                    rotateAmount = Vector3.Cross(direction, transform.up).z;
                    rb2d.angularVelocity = -rotateAmount * rotateSpeed;
                    rb2d.velocity = transform.up * speed;
                    break;
            }
        }
        else if (target == null)
        {
            destroyTime += Time.fixedDeltaTime;
        }
        if (destroyTime > 2)
        {
            Destroy(gameObject);
            //INSTANCIAR EXPLOSION
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(this.tag))
        {

        }
        else
        {

        }
    }
}
