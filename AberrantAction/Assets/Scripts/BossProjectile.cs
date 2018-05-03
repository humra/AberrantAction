using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour {

    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;
    private GameObject targetEnemy;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(targetEnemy == null)
        {
            Destroy(gameObject);
        }
        else
        {
            rb.velocity = (targetEnemy.transform.position - transform.position).normalized * speed;
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetTargetEnemy(GameObject enemy)
    {
        targetEnemy = enemy;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Obstructions"))
        {
            Destroy(gameObject);
        }
    }
}
