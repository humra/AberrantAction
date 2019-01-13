using System.Collections;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [SerializeField]
    private float maxHP = 100f;
    [SerializeField]
    private float regenFactorPerSecond = 1;
    [SerializeField]
    private float damageTakenMultiplier = 1f;
    private float currentHP;
    private PlayerController playerController;

	void Start () {
        playerController = (PlayerController)FindObjectOfType<PlayerController>();
        currentHP = maxHP;
        InvokeRepeating("RegenerateHealth", 0.5f, 0.5f);
	}


    public float GetCurrentHealth()
    {
        return currentHP;
    }

    private void RegenerateHealth()
    {
        if(currentHP < maxHP)
        {
            currentHP += regenFactorPerSecond / 2;

            if(currentHP > maxHP)
            {
                currentHP = maxHP;
            }
        }
    }

    public void RestoreHealth(float healthAmount)
    {
        currentHP += healthAmount;

        if(currentHP > maxHP)
        {
            currentHP = maxHP;
        }
    }
}