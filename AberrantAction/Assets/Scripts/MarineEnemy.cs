using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject attackType;
    [SerializeField]
    private GameObject projectileSpawnPoint;
    [SerializeField]
    private BossController target;
    [SerializeField]
    private int health = 20;

	void Start () {
        target = FindObjectOfType<BossController>();
        InvokeRepeating("Shoot", 1f, 3f);
	}
	
	void Update () {
		
	}

    private void Shoot()
    {
        GameObject instance = Instantiate(attackType, projectileSpawnPoint.transform.position, transform.rotation);
        
        Vector3 aimTarget = target.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = projectileSpawnPoint.transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            target.RemoveFromTargets(this.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("BossProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<BossProjectile>().GetDamage());
            Destroy(collision.gameObject);
        }
    }

    public int GetCurrentHealth()
    {
        return health;
    }
}
