using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private float maxHP = 100f;
    [SerializeField]
    private float regenFactorPerSecond = 1;
    [SerializeField]
    private float damageTakenMultiplier = 1f;
    private float currentHP;

	void Start () {
        currentHP = maxHP;
        InvokeRepeating("RegenerateHealth", 1f, 1f);
	}
	
	
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        currentHP -= Mathf.Floor(damage * damageTakenMultiplier);
        if(currentHP < 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameManager>().GameOver("PLAYER");
    }

    public float GetCurrentHealth()
    {
        return currentHP;
    }

    private void RegenerateHealth()
    {
        if(currentHP < maxHP)
        {
            currentHP += regenFactorPerSecond;

            if(currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("EnemyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyProjectileStats>().GetDamage());
            Destroy(collision.gameObject);
        }
    }
}
