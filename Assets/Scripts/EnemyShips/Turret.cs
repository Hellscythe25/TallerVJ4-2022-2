using AI.SteeringBehaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(CircleCollider2D))]
public class Turret : Entity
{
    private CircleCollider2D cc2d;
    [SerializeField] private float turretRange = 3;
    [SerializeField] NewPlayer player;
    public ENEMYSTATES states;

    public float cooldown = 1;
    public float cooldownTimer;
    new void Awake()
    {
        base.Awake();
        cc2d = GetComponentInChildren<CircleCollider2D>();
        player = FindObjectOfType<NewPlayer>();
    }
    new void Start()
    {
        base.Start();
        cc2d.isTrigger = true;
        cc2d.radius = turretRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
    }

    private void FixedUpdate()
    {
        if (health.CurrentHP() <= 0)
        {
            isAlive = false;
        }
        cooldownTimer -= Time.fixedDeltaTime;
        if (player != null)
        {

            switch (states)
            {
                case ENEMYSTATES.IDLE:
                                     
                    break;  

                case ENEMYSTATES.SHOOT:
                    // SI TENEMOS COOLDOWN, DISPARAR
                    rb2d.angularVelocity = 0;
                    rb2d.velocity = Vector2.zero;
                    if (cooldownTimer <= 0)
                    {
                        //Apuntar al player
                        direction = player.transform.position - this.transform.position;
                        direction = direction.normalized;
                        //DISPARAR
                        Shoot();
                        //RESETEAR EL CD
                        cooldownTimer = cooldown;
                    }
                   
                    
                    break;
                
            }
        }
    }

}
