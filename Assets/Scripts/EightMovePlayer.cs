using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EightMovePlayer : Entity
{
    float xMove;
    float yMove;
    Animator anim;
    Vector2 movement;
    public GameObject[] bulletSpawns;
    // 0 = DOWN
    // 1 = UP
    // 2 = LEFT-RIGHT
    // 3 = DOWN LEFT-RIGHT
    // 4 = UP LEFT-RIGHT
    private new void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }
    private new void Start()
    {
        base.Start();
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EightDirectionShoot(movement, xMove, yMove);
        }
    }
    private void FixedUpdate()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        yMove = Input.GetAxisRaw("Vertical");
        movement = new Vector2(xMove, yMove);
        movement = movement.normalized;
        //direction = movement;
        rb2d.velocity = movement * speed;
        anim.SetFloat("X", xMove);
        anim.SetFloat("Y", yMove);
    }

    protected void EightDirectionShoot(Vector2 dir, float x, float y)
    {
        int index = 0;
        if (x == 0 && y == -1)
        {
            index = 0;
        }
        else if (x == 0 && y == 1)
        {
            index = 1;
        }
        else if (x == 1 | x == -1 && y == 0)
        {
            index = 2;
        }
        else if (x == 1 | x == -1 && y == -1)
        {
            index = 3;
        }
        else if (x == 1 | x == -1 && y == 1)
        {
            index = 4;
        }

        if (dir == Vector2.zero)
        {
            dir = new Vector2(0, -1);
        }
        GameObject go = Instantiate(bullet, bulletSpawns[index].transform.position, Quaternion.identity);
        go.GetComponent<Bullet>().SetDirection(dir);
        go.tag = this.tag;
    }

}
