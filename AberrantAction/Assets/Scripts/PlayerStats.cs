using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private int maxHP = 100;
    [SerializeField]
    private int regenFactorPerSecond = 1;
    private int currentHP;

	void Start () {
        currentHP = maxHP;
        InvokeRepeating("RegenerateHealth", 1f, 1f);
	}
	
	
	void Update () {
		
	}

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if(currentHP < 0)
        {
            currentHP = 0;
        }
    }

    public int GetCurrentHealth()
    {
        return currentHP;
    }

    private void RegenerateHealth()
    {
        if(currentHP < maxHP)
        {
            currentHP += regenFactorPerSecond;

            if(currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }
}
