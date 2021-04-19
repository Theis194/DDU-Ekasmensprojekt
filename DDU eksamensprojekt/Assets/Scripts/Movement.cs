using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class Movement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    //public float speed;
    public float jump;
    public Rigidbody rb;

    public float speed = 10.0f;
    public float maxVelocityChange = 10.0f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    bool Grounded;

    public PlayerControls controls;

    public GameObject gun;

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        //controls.Disable();
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
        Vector3 targetVelocity = new Vector3(moveInput.x, 0, 0);
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        Vector3 velocity = rb.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;
        velocityChange.z = 0;
        rb.AddForce(velocityChange, ForceMode.Force);


        //rb.velocity = new Vector3(moveInput.x * speed, rb.velocity.y, rb.velocity.z);
    }

    public void BindControls(PlayerControls controls)
    {
        this.controls = controls;
        controls.Gameplay.Jump.performed += Jump_performed;
        this.controls.Enable();
        gun.GetComponent<Gun>().BindControls(controls);
    }
}
