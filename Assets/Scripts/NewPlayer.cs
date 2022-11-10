using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PLAYERINPUT { KEYBOARD, JOYSTICK};
public class NewPlayer : Entity
{
    [SerializeField] float boosterMult = 2;
    [SerializeField] float boosters = 10;
    bool isBoosting = false;
    bool isMoving = false;
    bool isDestroyed = false;
    float angleP;
    public ParticleSystem moveParticles, turboParticles;
    private PlayerHealthUI healthUI;
    public PLAYERINPUT inputType;
    public Joystick joystick;
    public GameObject deathParticles;
    new void Awake() 
    {
        base.Awake();
        healthUI = GetComponent<PlayerHealthUI>();
    }

    new void Start() 
    {
        base.Start();
        healthUI.SetHealth(health);
#if UNITY_EDITOR
        Debug.Log("Unity Editor");
#endif

#if UNITY_IOS
      Debug.Log("iOS");
#endif

#if UNITY_STANDALONE_OSX
        Debug.Log("Standalone OSX");
#endif

#if UNITY_STANDALONE_WIN
        Debug.Log("Standalone Windows");
#endif
    }

    private void Update()
    {
        switch (inputType) 
        {
            case PLAYERINPUT.KEYBOARD:
                if (Input.GetButton("Fire3") && boosters > 0)
                {
                    isBoosting = true;
                    boosters -= Time.deltaTime;
                }
                else
                {
                    isBoosting = false;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
                break;
            case PLAYERINPUT.JOYSTICK:
                break;
        }
        
        if (health.CurrentHP() <= 0)
        {
            isAlive = false;
        }

        if (!isAlive && !isDestroyed)
        {
            isDestroyed = true;
            Instantiate(deathParticles, this.transform.position, Quaternion.identity);
            rb2d.velocity = new Vector3();
            Invoke("DelayedDeath", 0.5f);           
        }
    }

    private void DelayedDeath() 
    {
        this.gameObject.SetActive(false);
    }
    // Se ejecuta cada 0.02 segundos
    void FixedUpdate()
    {
        Vector2 axis = new Vector2();
        if (isAlive)
        {
            switch (inputType)
            {
                case PLAYERINPUT.KEYBOARD:
                    //Look at mouse
                    float camDis = Camera.main.transform.position.y - this.transform.position.y;
                    Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));
                    angleP = Mathf.Atan2(mouse.y - this.transform.position.y, mouse.x - this.transform.position.x) * Mathf.Rad2Deg;
                    this.transform.rotation = Quaternion.Euler(0, 0, angleP - 90);

                    axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
                    break;
                case PLAYERINPUT.JOYSTICK:
                    axis = new Vector2(joystick.Horizontal, joystick.Vertical);
                    break;
            }

            axis = axis.normalized;

            rb2d.velocity = isBoosting ? axis * speed * boosterMult : axis * speed;
            boosters = (boosters < 0 ? boosters = 0 : boosters);
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction = direction.normalized;

            if (rb2d.velocity == Vector2.zero)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            if (isMoving)
            {
                moveParticles.gameObject.SetActive(true);
                moveParticles.Play();
            }
            else
            {
                moveParticles.Pause();
                moveParticles.gameObject.SetActive(false);
            }
            if (isBoosting)
            {
                turboParticles.gameObject.SetActive(true);
                turboParticles.Play();
            }
            else
            {
                turboParticles.Pause();
                turboParticles.gameObject.SetActive(false);
            }
        }
        
        
    }

    public override void TakeDamage(int dmg)
    {
        health.TakeDamage(dmg);
        healthUI.SetHealth(health);
    }
}
