using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveRemove : MonoBehaviour
{
    float timer = 2;
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Destroy(this.gameObject);
            timer = 2;
        }
    }
}
