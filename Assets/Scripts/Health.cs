using System;

[System.Serializable]
public class Health
{
    public float hitPoints;
    public float shields;
    public float maxHealth;
    public float maxShields;

    public Health(float hp, float shields)
    {
        this.maxHealth = hp;
        this.maxShields = shields;
        this.hitPoints = maxHealth;
        this.shields = maxShields;
    }
    public Health() 
    {

    }

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
    public void SetMaxHealth(float hp) 
    {
        maxHealth = hp;
    }
    public void SetShields(float s) 
    {
        shields = s;
    }
    public void SetMaxShields(float s) 
    {
        maxShields = s;
    }
    public float GetMaxHealth() 
    {
        return maxHealth;
    }
    public float GetMaxShields() 
    {
        return maxShields;
    }
    public float CurrentHP() 
    {
        return hitPoints;
    }
    public float CurrentShields()
    {
        return shields;
    } 
}
