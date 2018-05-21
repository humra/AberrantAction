using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [SerializeField]
    private float maxHP = 1000f;
    [SerializeField]
    private float damageTakenMultiplier = 1f;
    private float currentHP;
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
        FindObjectOfType<GameManager>().GameOver("BOSS");
    }

    public void TakeDamage(float damage)
    {
        currentHP -= Mathf.Floor(damage * damageTakenMultiplier);
        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public float GetCurrentHealth()
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

    public void StopAllActions()
    {
        CancelInvoke();
    }
}
