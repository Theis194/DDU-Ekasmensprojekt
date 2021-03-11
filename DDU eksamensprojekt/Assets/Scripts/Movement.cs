using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    public float speed;
    public float jump;
    public Rigidbody rb;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool Grounded;

    PlayerControls controls;
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Jump.performed += Jump_performed;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Jump_performed(InputAction.CallbackContext obj)
    {
        Grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayerMask);
        if (Grounded)
        {
            rb.AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector2 moveInput = controls.Gameplay.Move.ReadValue<Vector2>();
        rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, rb.velocity.z);
    }
}
