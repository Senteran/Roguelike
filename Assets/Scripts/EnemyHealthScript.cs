using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthScript : MonoBehaviour
{
    public float maxHealth = 2;
    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        maxHealth--;
        if (maxHealth <= 0) Death();
    }

    void Death()
    {
        Debug.Log("Enemy died");
        Destroy(gameObject);
    }
}
