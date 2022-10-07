using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.SteeringBehaviours;

public class Tank : Entity
{
    public ENEMYSTATES states;
    [SerializeField] float maxForce = 2;
    [SerializeField] float turnRate = 50;

    [SerializeField] NewPlayer player;

    public GameObject missile;
    public float cooldown = 2;
    public float cooldownTimer;
    private float angle = 30;

    new void Awake()
    {
        base.Awake();
        player = FindObjectOfType<NewPlayer>();
    }
    new void Start()
    {
        base.Start();
        states = ENEMYSTATES.IDLE;
        cooldownTimer = cooldown;
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
                    //print(Vector3.Distance(player.transform.position, this.transform.position));
                    // SABER SI EL PLAYER ESTA CERCA, SI ESTÁ CERCA ==> PERSEGUIR
                    if (Vector3.Distance(player.transform.position, this.transform.position) < 10)
                    {
                        states = ENEMYSTATES.SEEK;
                    }
                    // SI MI HP ESTA BAJO, CORRER!!!
                    if (health.CurrentHP() < 2)
                    {
                        states = ENEMYSTATES.FLEE;
                    }
                    break;
                case ENEMYSTATES.SEEK:
                    // PERSEGUIR AL PLAYER
                    rb2d.velocity = SteeringBehaviours.Seek(this.transform.position, rb2d.velocity, player.transform.position, speed, maxForce);

                    // SI EL PLAYER ESTÁ CERCA, CAMBIAR A ESTADO DE DISPARO!
                    if (Vector3.Distance(player.transform.position, this.transform.position) < 9)
                    {
                        states = ENEMYSTATES.SHOOT;
                    }
                    // SI MI HP ESTÁ BAJO, CORRER!
                    if (health.CurrentHP() < 2)
                    {
                        states = ENEMYSTATES.FLEE;
                    }
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
                        CreateMissile();
                        //RESETEAR EL CD
                        cooldownTimer = cooldown;
                    }
                    // SI PLAYER SE ENCUENTRA MUY LEJOS, VOLVER AL ESTADO DE PERSEGUIR (SEEK)
                    if (Vector3.Distance(player.transform.position, this.transform.position) > 2)
                    {
                        states = ENEMYSTATES.SEEK;
                    }
                    // SI MI HP ESTÁ BAJO, CORRER!
                    if (health.CurrentHP() < 2)
                    {
                        states = ENEMYSTATES.FLEE;
                    }
                    break;
                case ENEMYSTATES.FLEE:
                    // CORREMOS
                    if (Vector3.Distance(player.transform.position, this.transform.position) < 11)
                    {
                        rb2d.velocity = SteeringBehaviours.Flee(this.transform.position, rb2d.velocity, player.transform.position, speed, maxForce);
                    }
                    // SI EL PLAYER ESTÁ MUY LEJOS ==> DISPARO LARGO (LONGSHOT)
                    break;
                case ENEMYSTATES.LONGSHOT:
                    // DISPARAR MISILES / DISPARAR DOBLE
                    break;
                case ENEMYSTATES.WANDER:
                    // ESTAR A LA ESPERA DEL PLAYER
                    // CAMINAR EN UN RADIO DETERMINADO
                    break;
            }
        }

        if (player != null && Vector3.Distance(player.transform.position, this.transform.position) < 6 && states != ENEMYSTATES.FLEE)
        {
            transform.up = (player != null ? Vector3.Lerp(transform.up, (player.transform.position - this.transform.position), turnRate) : transform.up);
        }
        if (states == ENEMYSTATES.FLEE)
        {
            transform.up = (player != null ? Vector3.Lerp(transform.up, (this.transform.position - player.transform.position), turnRate) : transform.up);
        }
        if (!isAlive)
        {
            this.gameObject.SetActive(false);
        }


    }

    void CreateMissile() 
    {
        GameObject go = Instantiate(missile, bulletSpawn.position, Quaternion.identity);
        go.GetComponent<HomingMissile>().Initialize(5, player.transform, angle);
        go.tag = this.tag;
        angle *= -1;
    }
}
