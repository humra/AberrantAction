using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [SerializeField]
    private int maxHP = 1000;
    private int currentHP;

	void Start () {
        currentHP = maxHP;
	}
	
	void Update () {
		if(currentHP == 0)
        {
            Die();
        }
	}

    private void Die()
    {
        //TO-DO
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("EnemyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyProjectileStats>().GetDamage());
            Destroy(collision.gameObject);
        }
    }
}
