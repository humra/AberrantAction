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

    public void TakeDamage(float damage)
    {
        if(damage == 0)
        {
            return;
        }

        currentHP -= Mathf.Floor(damage * damageTakenMultiplier);

        FindObjectOfType<PlayerController>().DamageTakenSoundEffect();

        if(currentHP < 0)
        {
            currentHP = 0;
            Die();
        }
    }

    private void Die()
    {
        FindObjectOfType<GameManager>().GameOver("PLAYER");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("EnemyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyProjectileStats>().GetDamage());
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("SlowingProjectile"))
        {
            SlowingProjectileStats projectile = (SlowingProjectileStats)collision.gameObject.GetComponent<SlowingProjectileStats>();
            AdjustMovementSpeed(projectile.GetDuration(), projectile.GetSlow());
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag.Equals("HealthGlobe"))
        {
            RestoreHealth(collision.gameObject.GetComponent<HealthGlobe>().GetHealthRestored());
            FindObjectOfType<PlayerController>().HealthGlobeSoundEffect();
            Destroy(collision.gameObject);
        }
    }

    private void AdjustMovementSpeed(float duration, float speedFactor)
    {
        StopAllCoroutines();
        playerController.SetMovementSpeed(speedFactor);
        StartCoroutine(MovementSpeedTimeout(duration));
    }

    IEnumerator MovementSpeedTimeout(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerController.ResetMovementSpeed();
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