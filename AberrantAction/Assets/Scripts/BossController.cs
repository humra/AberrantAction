using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [SerializeField]
    private int maxHP = 1000;
    private int currentHP;
    [SerializeField]
    private List<GameObject> enemies;
    [SerializeField]
    private BossProjectile bossAttack;
    [SerializeField]
    private GameObject firingPoint;
    [SerializeField]
    private float attackRate = 5f;

	void Start () {
        currentHP = maxHP;
        enemies = new List<GameObject>();
        InvokeRepeating("Attack", 3f, attackRate);
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

    public void AddNewEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    private void Attack()
    {
        if(enemies.Count == 0)
        {
            return;
        }

        GameObject target = enemies[UnityEngine.Random.Range(0, enemies.Count)];

        BossProjectile instance = Instantiate(bossAttack, firingPoint.transform.position, firingPoint.transform.rotation);
        instance.SetTargetEnemy(target);
    }

    public void RemoveFromTargets(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    public int GetNumberOfEnemies()
    {
        return enemies.Count;
    }
}
