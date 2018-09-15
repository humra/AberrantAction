using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    private float defaultMovementSpeed;
    [SerializeField]
    private float interpolation;
    private Vector2 facingDirection;
    private Rigidbody2D rb;
    [SerializeField]
    private float stunCooldown = 1f;
    private float stunTimeStamp;
    private bool isDisabled = false;
    [SerializeField]
    private AudioClip healthGlobeSoundEffect;
    [SerializeField]
    private AudioClip damageTakenSoundEffect;
    [SerializeField]
    private GameObject stunIndicator;
	
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        facingDirection = Vector2.zero;
        stunTimeStamp = Time.time;
        defaultMovementSpeed = movementSpeed;
	}
	
	
	void Update () {

        if(isDisabled)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 movementVector = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * movementSpeed, interpolation),
        Mathf.Lerp(0, Input.GetAxis("Vertical") * movementSpeed, interpolation));
        rb.velocity = movementVector;

        if(Time.time >= stunTimeStamp && stunIndicator.active == false)
        {
            stunIndicator.SetActive(true);
        }

        if (movementVector != Vector2.zero)
        {
            facingDirection = rb.velocity.normalized;
        }
        transform.up = facingDirection;
    }

    public bool IsStunOnCooldown()
    {
        if(stunTimeStamp <= Time.time)
        {
            return false;
        }

        return true;
    }

    public void StunActivated()
    {
        stunTimeStamp = Time.time + stunCooldown;
        stunIndicator.SetActive(false);
    }

    public void HealthGlobeSoundEffect()
    {
        GetComponent<AudioSource>().clip = healthGlobeSoundEffect;
        GetComponent<AudioSource>().Play();
    }

    public void DamageTakenSoundEffect()
    {
        GetComponent<AudioSource>().clip = damageTakenSoundEffect;
        GetComponent<AudioSource>().Play();
    }

    public void SetMovementSpeed(float factor)
    {
        movementSpeed *= factor;
    }

    public void ResetMovementSpeed()
    {
        movementSpeed = defaultMovementSpeed;
    }
}
