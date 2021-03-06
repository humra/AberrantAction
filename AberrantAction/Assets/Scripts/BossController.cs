﻿using System;
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
    private List<Enemy> enemies;
    [SerializeField]
    private BossProjectile bossAttack;
    [SerializeField]
    private GameObject firingPoint;
    [SerializeField]
    private float attackRate = 5f;
    [SerializeField]
    private float initialFiringDelay = 3f;
    private float firingTimestamp;
    private AudioSource shootSoundEffectSrc;
    private AudioSource damageTakenSoundEffectSrc;

    void Start () {
        currentHP = maxHP;
        enemies = new List<Enemy>();
        firingTimestamp = Time.time + initialFiringDelay;
        var audioSources = GetComponents<AudioSource>();
        shootSoundEffectSrc = audioSources[0];
        damageTakenSoundEffectSrc = audioSources[1];
	}
	
	void Update () {
		if(currentHP == 0)
        {
            Die();
            return;
        }

        if(firingTimestamp <= Time.time)
        {
            Attack();
        }
	}

    private void Die()
    {
        FindObjectOfType<GameManager>().GameOver("BOSS");
    }

    public void TakeDamage(float damage)
    {
        currentHP -= Mathf.Floor(damage * damageTakenMultiplier);

        damageTakenSoundEffectSrc.Play();

        if (currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public float GetCurrentHealth()
    {
        return currentHP;
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("EnemyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyProjectileStats>().GetDamage());
            Destroy(collision.gameObject);
        }
    }

    public void AddNewEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    private void Attack()
    {
        if(enemies.Count == 0)
        {
            return;
        }

        Enemy target = enemies[UnityEngine.Random.Range(0, enemies.Count)];

        BossProjectile instance = Instantiate(bossAttack, firingPoint.transform.position, firingPoint.transform.rotation);
        instance.SetTargetEnemy(target.gameObject);

        //GetComponent<AudioSource>().clip = shootSoundEffect;
        //GetComponent<AudioSource>().Play();
        shootSoundEffectSrc.Play();

        firingTimestamp = Time.time + attackRate;
    }

    public void RemoveFromTargets(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public int GetNumberOfEnemies()
    {
        return enemies.Count;
    }
}
