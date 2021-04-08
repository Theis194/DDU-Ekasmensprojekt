using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10;
    public float timer = 3;

    public float power;
    public float radius;
    public float upForce = 1f;

    public Rigidbody rb;

    public ParticleSystem shockwave;

    Gun gun;
    void Start()
    {
        gun = GameObject.Find("gun").GetComponent<Gun>();
        
        if(gun != null)
        {
            //Debug.Log(other.direction);
        }

        rb.AddForce(gun.direction * bulletSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        shockwave.Play();

        foreach (Collider hit in colliders)
        {
            Rigidbody rigidbody = hit.GetComponent<Rigidbody>();

            if (rigidbody != null)
            {
                rigidbody.AddExplosionForce(power, explosionPos, radius, upForce, ForceMode.Impulse);
            }
        }

        shockwave.Stop();
        Destroy(this.gameObject);
    }

    //this is how you draw gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}