using UnityEngine;

public class SlowingProjectileStats : MonoBehaviour {

    [SerializeField]
    private float speedFactor = 0.75f;
    [SerializeField]
    private float slowDuration = 3f;
    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("IncreaseSpeed", 2f, 2f);
    }

    public float GetSlow()
    {
        return speedFactor;
    }

    public float GetDuration()
    {
        return slowDuration;
    }

    public void Update()
    {
        rb.velocity = (GameObject.Find("Player").transform.position - transform.position).normalized * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Obstructions"))
        {
            Destroy(this.gameObject);
        }
    }

    private void IncreaseSpeed()
    {
        speed *= 1.1f;
    }
}
