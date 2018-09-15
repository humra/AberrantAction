using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{

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
    private float stunDuration;
    private PlayerController player;
    [SerializeField]
    private float stunRange;
    [SerializeField]
    private SpriteRenderer stunSprite;
    [SerializeField]
    private float healthGlobeValue = 15f;
    [SerializeField]
    private GameObject droppedGlobe;
    [SerializeField]
    private GameObject healthGlobeDropIndicator;
    private bool willDropHealthGlobe = false;
    [SerializeField]
    private GameObject firingCue;
    [SerializeField]
    private float cueDuration = 2f;
    private float firingTimestamp;
    [SerializeField]
    private AudioClip damageTakenSoundEffect;
    [SerializeField]
    private AudioClip attackPowerupSoundEffect;
    [SerializeField]
    private AudioClip deathSoundEffect;
    [SerializeField]
    private AudioClip stunSoundEffect;

    void Start()
    {
        target = FindObjectOfType<BossController>();
        player = FindObjectOfType<PlayerController>();
        stunSprite.enabled = isStunned;
        healthGlobeDropIndicator.SetActive(willDropHealthGlobe);
        firingTimestamp = Time.time + initialFiringDelayInSeconds;
    }

    private void Update()
    {
        if(firingTimestamp - cueDuration <= Time.time && !firingCue.active)
        {
            GetComponent<AudioSource>().clip = attackPowerupSoundEffect;
            GetComponent<AudioSource>().Play();

            ShowFiringCue();
        }

        if(firingTimestamp <= Time.time && !isStunned)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (isStunned)
        {
            return;
        }

        FireProjectile();
    }

    private void ShowFiringCue()
    {
        firingCue.SetActive(true);
    }

    private void FireProjectile()
    {
        GameObject instance = Instantiate(attackType, projectileSpawnPoint.transform.position, transform.rotation);

        Vector3 aimTarget = target.transform.position;
        aimTarget.z = 0f;
        Vector3 objPos = projectileSpawnPoint.transform.position;
        aimTarget.x = aimTarget.x - objPos.x;
        aimTarget.y = aimTarget.y - objPos.y;
        float angle = Mathf.Atan2(aimTarget.y, aimTarget.x) * Mathf.Rad2Deg;

        instance.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        firingCue.SetActive(false);

        firingTimestamp = Time.time + fireRateInSeconds;
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.Floor(damage * damageTakenMultiplier);

        GetComponent<AudioSource>().clip = damageTakenSoundEffect;
        GetComponent<AudioSource>().Play();

        if (health <= 0)
        {
            target.RemoveFromTargets(this);

            if (willDropHealthGlobe)
            {
                DropHealthGlobe();
            }

            player.GetComponent<AudioSource>().clip = deathSoundEffect;
            player.GetComponent<AudioSource>().Play();

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

    private void OnMouseDown()
    {
        if (Vector2.Distance(this.gameObject.transform.position, player.gameObject.transform.position) <= stunRange && !player.IsStunOnCooldown())
        {
            StartCoroutine(StunDisable(stunDuration));
            player.StunActivated();
            GetComponent<AudioSource>().clip = stunSoundEffect;
            GetComponent<AudioSource>().Play();
            firingTimestamp = Time.time + fireRateInSeconds;
            firingCue.SetActive(false);
        }
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
