using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaBlover : Blover
{
    public override void SpecialUpdate()
    {
        timeToKill -= Time.deltaTime;
        if (timeToKill <= 0)
        {
            Blow();
            timeToKill = 20f;
        }
    }
    public override void Blow()
    {
        base.Blow();
        anim.SetTrigger("blow");
    }
}
