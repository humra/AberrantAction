using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    private float defaultMovementSpeed;
    [SerializeField]
    private float interpolation;
    private Vector2 facingDirection;
    private Rigidbody2D rb;
    private bool isDisabled = false;
    
	
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        facingDirection = Vector2.zero;
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


        if (movementVector != Vector2.zero)
        {
            facingDirection = rb.velocity.normalized;
        }
        transform.up = facingDirection;
    }
}
