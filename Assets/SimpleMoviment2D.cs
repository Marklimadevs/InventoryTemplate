using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMoviment2D : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] float horizontal;
    [SerializeField] float vertical;
    [SerializeField] float magnitude;
    float moveLimiter = 0.7f;

    public float runSpeed = 20.0f;

    void Start()
    {
        if (body == null)
        {
            body = GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        // A D <- -> 
        horizontal = Input.GetAxisRaw("Horizontal"); 
        // W S 
        vertical = Input.GetAxisRaw("Vertical"); 

        magnitude = new Vector2(horizontal,vertical).magnitude;
    }

    void FixedUpdate()
    {
        //IS MOVING
        if (horizontal != 0 && vertical != 0) 
        {            
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        if (body != null)
        {
            body.velocity = new Vector2(horizontal * runSpeed  , vertical * runSpeed);
        }
    }
}
