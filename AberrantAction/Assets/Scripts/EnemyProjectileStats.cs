using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileStats : MonoBehaviour {

    [SerializeField]
    private int damage = 10;
    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;

    public int GetDamage()
    {
        return damage;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = (GameObject.Find("Boss").transform.position - transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Obstructions"))
        {
            Destroy(this.gameObject);
        }
    }
}
