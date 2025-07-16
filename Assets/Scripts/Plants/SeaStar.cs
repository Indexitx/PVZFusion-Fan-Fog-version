using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaStar : Starfruit
{
    public float timering;
    public bool isGrowen = true;
    public override void SpecialUpdate()
    {
        base.SpecialUpdate();
        timering -= Time.deltaTime;
        if (timering <= 0 && !isGrowen)
        {
            anim.SetTrigger("grow");
            isGrowen = true;
        }
    }
}
