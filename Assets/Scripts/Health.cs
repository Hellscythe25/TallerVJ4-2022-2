using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float hitPoints;
    [SerializeField] float shields;

    public void TakeDamage(float damage) 
    {
        if (shields > 0)
        {
            shields -= damage;
        }
        else
        {
            hitPoints -= damage;
        }
    }

    public void SetHealth(float hp) 
    {
        hitPoints = hp;
    }
    public void SetShields(float s) 
    {
        shields = s;
    }
    public float CurrentHP() 
    {
        return hitPoints;
    }
}
