using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneLaser : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent<Zombie>(out var z))
        {
            if (z.isFlying)
            {
                z.TakeDamage(1, 400);
            }
            else
            {
                z.TakeDamage(1, 200);
            }
        }
    }
}
