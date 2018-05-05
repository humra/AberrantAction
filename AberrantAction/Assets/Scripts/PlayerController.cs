using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float interpolation;
    private Vector2 facingDirection;
    private Rigidbody2D rb;
	
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        facingDirection = Vector2.zero;
	}
	
	
	void Update () {

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
