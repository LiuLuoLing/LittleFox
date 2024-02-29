using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    //private Collider2D Coll;

    public Transform upperpoint;
    public Transform lowerpoint;
    public float Speed;
    private float uppery, lowery;

    private bool IsUp;

   protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
       //Coll = GetComponent<Collider2D>();
        uppery = upperpoint.position.y;
        lowery = lowerpoint.position.y;
        Destroy(upperpoint.gameObject);
        Destroy(lowerpoint.gameObject);
    }


    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (IsUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, Speed);
            if (transform.position.y > uppery)
            {
                IsUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -Speed);
            if (transform.position.y < lowery)
            {
                IsUp = true;
            }
        }
    }
}
