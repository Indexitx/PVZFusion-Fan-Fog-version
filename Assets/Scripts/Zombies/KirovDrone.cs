using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirovDrone : AirshipZombie
{
    public override void TakeDamage(int theDamageType, int theDamage)
    {
        TD(theDamageType, theDamage);
    }
    protected override void MoveUpdate()
    {
        MU();
        if (isWalking)
        {
            Knockback(-Time.deltaTime * theSpeed / 5);
        }

    }
    public override void Steal()
    {
        TakeDamage(0, 1000000);
    }
}
