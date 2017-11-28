using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [HideInInspector]
    public float speed;
    public bool alive = true;
    public float startSpeed = 1.25f;
    public float health = 100;

    public int worth = 50;


    public GameObject deathEffect;

    private void Start()
    {
        
        speed = startSpeed;
    }

    public void TakeDamage (float amount)
    {
        health -= amount;

        if (health <= 0 && alive)
        {
            alive = false;
            Die();
        }
    }

    public void Slow (float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        PlayerStats.Money += worth;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        Destroy(gameObject);
        
    }


}
