using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayer : Entity
{
    [SerializeField] float boosterMult = 2;
    [SerializeField] float boosters = 10;
    bool isBoosting = false;
    bool isMoving = false;
    float angleP;
    public ParticleSystem moveParticles, turboParticles;

    new void Awake() 
    {
        base.Awake();
    }

    new void Start() 
    {
        base.Start();
        
    }

    private void Update()
    {
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
        if (health.CurrentHP() <= 0)
        {
            isAlive = false;
        }

        if (!isAlive)
        {
            this.gameObject.SetActive(false);
        }
    }    

    // Se ejecuta cada 0.02 segundos
    void FixedUpdate()
    {
        //Look at mouse
        float camDis = Camera.main.transform.position.y - this.transform.position.y;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));
        angleP = Mathf.Atan2(mouse.y - this.transform.position.y, mouse.x - this.transform.position.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angleP - 90);

        Vector2 axis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        axis = axis.normalized;
        
        rb2d.velocity = isBoosting ? axis * speed * boosterMult : axis * speed;

        if (rb2d.velocity == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }

        boosters = (boosters < 0 ? boosters = 0 : boosters);

        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        direction = direction.normalized;


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
