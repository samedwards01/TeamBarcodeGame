using System;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"),
       Input.GetAxis("Vertical")) * speed;
    }
    // Update is called once per frame
    void Update()
    {
    }
}
