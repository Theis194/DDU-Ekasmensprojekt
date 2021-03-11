using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    public float speed;
    public float jump;
    public Rigidbody rb;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool Grounded;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        //Jump
        if(Input.GetButtonDown("Jump") && Grounded)
        {
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        //Floats til input af Horizontal og Vertikal movement
        float mH = Input.GetAxis("Horizontal");
        float mV = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(mH * speed, rb.velocity.y, rb.velocity.z);
    }
}
