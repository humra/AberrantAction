﻿using UnityEngine;

public class EnemyProjectileStats : MonoBehaviour {

    [SerializeField]
    private float damage = 10f;
    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;

    public float GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        rb.velocity = (GameObject.Find("Queen").transform.position - transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstructions"))
        {
            Destroy(this.gameObject);
        }
    }
}
