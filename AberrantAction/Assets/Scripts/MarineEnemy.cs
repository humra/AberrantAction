using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarineEnemy : MonoBehaviour {

    [SerializeField]
    private GameObject attackType;
    [SerializeField]
    private GameObject projectileSpawnPoint;
    [SerializeField]
    private GameObject target;

	void Start () {
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
}
