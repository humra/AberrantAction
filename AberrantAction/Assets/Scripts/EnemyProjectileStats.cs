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
        //rb.AddForce(transform.up * speed);
        //rb.velocity = new Vector2(speed, rb.velocity.y);
        
    }

    private void Update()
    {
        //rb.velocity = transform.forward * speed;
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
