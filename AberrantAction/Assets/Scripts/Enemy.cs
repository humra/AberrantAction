﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private GameObject attackType;
    [SerializeField]
    private GameObject projectileSpawnPoint;
    private BossController target;
    [SerializeField]
    private float health = 20;
    [SerializeField]
    private float damageTakenMultiplier = 1f;
    [SerializeField]
    private float fireRateInSeconds = 1f;
    [SerializeField]
    private float initialFiringDelayInSeconds = 3f;
    private bool isStunned = false;
    [SerializeField]
    private float stunDuration = 3f;
    private PlayerController player;
    [SerializeField]
    private float stunRange = 5f;
    [SerializeField]
    private SpriteRenderer stunSprite;
    [SerializeField]
    private float healthGlobeValue = 15f;
    [SerializeField]
    private GameObject droppedGlobe;
    private bool willDropHealthGlobe = false;

    void Start()
    {
        target = FindObjectOfType<BossController>();
        player = FindObjectOfType<PlayerController>();
        stunSprite.enabled = isStunned;
        InvokeRepeating("Shoot", initialFiringDelayInSeconds, fireRateInSeconds);
    }

    private void Shoot()
    {
        if (isStunned)
        {
            return;
        }

        GameObject instance = Instantiate(attackType, projectileSpawnPoint.transform.position, transform.rotation);

        Vector3 aimTarget = target.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = projectileSpawnPoint.transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.Floor(damage * damageTakenMultiplier);
        if (health <= 0)
        {
            target.RemoveFromTargets(this.gameObject);

            if(willDropHealthGlobe)
            {
                DropHealthGlobe();
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("BossProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<BossProjectile>().GetDamage());
            Destroy(collision.gameObject);
        }
    }

    public float GetCurrentHealth()
    {
        return health;
    }

    private void OnMouseDown()
    {
        if (Vector2.Distance(this.gameObject.transform.position, player.gameObject.transform.position) <= stunRange && !player.IsStunOnCooldown())
        {
            StartCoroutine(StunDisable(stunDuration));
            player.StunActivated();
            GetComponent<AudioSource>().Play();
        }
    }

    public void StopAllActions()
    {
        CancelInvoke();
    }

    private IEnumerator StunDisable(float stunTime)
    {
        isStunned = true;
        UpdateStun();
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        UpdateStun();
    }

    private void UpdateStun()
    {
        stunSprite.enabled = isStunned;
    }

    public void SetWillDropHealthGlobe(bool willDropGlobe)
    {
        this.willDropHealthGlobe = willDropGlobe;
    }

    private void DropHealthGlobe()
    {
        GameObject globe = Instantiate(droppedGlobe, projectileSpawnPoint.transform.position, Quaternion.identity);
        globe.GetComponent<HealthGlobe>().SetHealthRestored(healthGlobeValue);
    }
}