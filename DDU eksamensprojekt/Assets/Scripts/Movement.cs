using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    PlayerControls controls;
    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Jump.performed += Jump_performed;
        controls.Gameplay.Restart.performed += Restart_performed;
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

    private void Restart_performed (InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("Main");
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
}
