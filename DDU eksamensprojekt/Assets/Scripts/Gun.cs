using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject svivel;
    public GameObject gun;

    public PlayerControls controls;

    public GameObject Bullet;
    public GameObject firePosition;

    public Vector3 direction;
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 gunPosition = controls.Gameplay.Rotation.ReadValue<Vector2>() * 1f;
        
        if(Vector2.Distance(controls.Gameplay.Rotation.ReadValue<Vector2>(),new Vector2(0,0)) == 1)
        {
            gun.transform.localPosition = gunPosition;
        }
        gun.transform.LookAt(svivel.transform.position);

        float x = firePosition.transform.position.x - svivel.transform.position.x;
        float y = firePosition.transform.position.y - svivel.transform.position.y;

        //Debug.Log("X: " + x + "Y: " + y + "Z: " + z);

        direction = new Vector3(x, y, 0);
        //Debug.Log(direction);
    }

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Shoot.performed += Shoot_performed;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        Instantiate(Bullet, firePosition.transform.position, Quaternion.identity);
    }
}
