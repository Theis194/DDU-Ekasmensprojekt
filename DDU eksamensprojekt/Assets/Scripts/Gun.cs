using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public GameObject svivel;
    public GameObject gun;
    public GameObject Bullet;
    public GameObject firePosition;

    public PlayerControls controls;

    public Vector3 direction;

    float cooldown;
    public float cooldownLength = 0.5f;

    private bool readyToShoot = false;

    private void Start()
    {
        cooldown = cooldownLength;
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

        direction = new Vector3(x, y, 0);
    }

    private void Update()
    {
        if(readyToShoot == false && cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0)
            {
                readyToShoot = true;
            }
        }
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
        if(readyToShoot == true)
        {
            GameObject bullet = Instantiate(Bullet, firePosition.transform.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().gun = GetComponent<Gun>();
            cooldown = cooldownLength;
            readyToShoot = false;
        }
    }

    public void BindControls(PlayerControls controls)
    {
        this.controls = controls;
        controls.Gameplay.Shoot.performed += Shoot_performed;
        this.controls.Enable();
    }
}
