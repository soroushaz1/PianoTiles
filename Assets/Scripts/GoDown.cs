using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoDown : MonoBehaviour
{
    private Vector2 velocity;
    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        velocity = new Vector2(0f, -5f);
    }

    void FixedUpdate()
    {
        //transform.position = transform.position + (Vector3.down * (speed * Time.deltaTime));
        rb2D.MovePosition(rb2D.position + velocity * Time.fixedDeltaTime);

    }
}
